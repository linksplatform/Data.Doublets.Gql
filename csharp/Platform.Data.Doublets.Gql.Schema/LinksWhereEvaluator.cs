using GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Data.Doublets.Gql.Schema
{
    /// <summary>Evaluates a complete predicate against one stable graph snapshot.</summary>
    internal sealed class LinksWhereEvaluator
    {
        private const int MaximumDepth = 32;
        private readonly Links[] _all;
        private readonly Dictionary<long, Links> _byId;
        private readonly Dictionary<long, Links[]> _outgoing;
        private readonly Dictionary<long, Links[]> _incoming;

        public LinksWhereEvaluator(IEnumerable<Links> links)
        {
            _all = links.ToArray();
            _byId = _all.ToDictionary(link => link.id);
            _outgoing = _all.GroupBy(link => link.from_id ?? 0).ToDictionary(group => group.Key, group => group.ToArray());
            _incoming = _all.GroupBy(link => link.to_id ?? 0).ToDictionary(group => group.Key, group => group.ToArray());
        }

        public static void Validate(LinksBooleanExpression? where, int depth = 0)
        {
            if (where == null) return;
            if (depth > MaximumDepth)
            {
                throw new ExecutionError($"Where nesting exceeds {MaximumDepth} levels.");
            }
            if (where._by_group != null || where._by_item != null || where._by_path_item != null || where._by_root != null
                || where.bool_exp != null || where.number != null || where.@string != null || where.type != null || where.type_id != null)
            {
                throw new ExecutionError("Type, materialized path, and value-table predicates are not supported by the links store.");
            }
            foreach (var child in new[] { where._not, where.from, where.to, where.type, where.@in, where.@out })
            {
                Validate(child, depth + 1);
            }
            foreach (var group in new[] { where._and, where._or })
            {
                if (group == null) continue;
                foreach (var child in group)
                {
                    if (child == null) throw new ExecutionError("Logical filter groups cannot contain null entries.");
                    Validate(child, depth + 1);
                }
            }
        }

        public List<Links> Select(LinksBooleanExpression? where, long? forceFromId, long? forceToId)
        {
            IEnumerable<Links> candidates = _all;
            // Top-level equalities are conjunctive, so these indexes can narrow
            // candidates without dropping any alternatives within an OR group.
            if (where?.id?._eq is long id)
            {
                candidates = _byId.TryGetValue(id, out var link) ? new[] { link } : Array.Empty<Links>();
            }
            else if ((forceFromId ?? where?.from_id?._eq) is long source)
            {
                candidates = Related(_outgoing, source);
            }
            else if ((forceToId ?? where?.to_id?._eq) is long target)
            {
                candidates = Related(_incoming, target);
            }
            // Materialize before any caller mutates the store. Nested predicates
            // and all selected rows must observe the same pre-mutation graph.
            return candidates.Where(link => (!forceFromId.HasValue || link.from_id == forceFromId)
                && (!forceToId.HasValue || link.to_id == forceToId)
                && Matches(link, where)).ToList();
        }

        private bool Matches(Links link, LinksBooleanExpression? where)
        {
            if (where == null) return true;
            // Every sibling is an AND condition. A logical group must never
            // return early and bypass scalar or relationship siblings.
            return (where._and == null || where._and.All(child => Matches(link, child)))
                && (where._or == null || where._or.Any(child => Matches(link, child)))
                && (where._not == null || !Matches(link, where._not))
                && Compare(link.id, where.id)
                && Compare(link.from_id, where.from_id)
                && Compare(link.to_id, where.to_id)
                && (where.from == null || MatchesReference(link.from_id, where.from))
                && (where.to == null || MatchesReference(link.to_id, where.to))
                && (where.@out == null || Related(_outgoing, link.id).Any(child => Matches(child, where.@out)))
                && (where.@in == null || Related(_incoming, link.id).Any(child => Matches(child, where.@in)));
        }

        private bool MatchesReference(long? id, LinksBooleanExpression where)
        {
            return id.HasValue && id.Value != 0 && _byId.TryGetValue(id.Value, out var link) && Matches(link, where);
        }

        private static Links[] Related(Dictionary<long, Links[]> index, long id)
        {
            return index.TryGetValue(id, out var links) ? links : Array.Empty<Links>();
        }

        private static bool Compare(long? value, LongComparisonExpression? comparison)
        {
            if (comparison == null) return true;
            var number = value ?? 0;
            // Keep signed comparisons signed; casting a negative predicate to
            // an unsigned native address can turn it into a wildcard constant.
            return (!comparison._eq.HasValue || number == comparison._eq.Value)
                && (!comparison._neq.HasValue || number != comparison._neq.Value)
                && (!comparison._gt.HasValue || number > comparison._gt.Value)
                && (!comparison._gte.HasValue || number >= comparison._gte.Value)
                && (!comparison._lt.HasValue || number < comparison._lt.Value)
                && (!comparison._lte.HasValue || number <= comparison._lte.Value)
                && (comparison._in == null || comparison._in.Contains(number))
                && (comparison._nin == null || !comparison._nin.Contains(number))
                && (!comparison._is_null.HasValue || comparison._is_null.Value == (!value.HasValue || number == 0));
        }
    }
}
