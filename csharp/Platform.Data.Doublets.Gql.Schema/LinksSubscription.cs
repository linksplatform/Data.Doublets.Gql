using GraphQL;
using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Input;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksSubscription : ObjectGraphType
    {
        private readonly ISubject<Links> _linkCreated = new Subject<Links>();
        private readonly ISubject<Links> _linkUpdated = new Subject<Links>();
        private readonly ISubject<ulong> _linkDeleted = new Subject<ulong>();

        public LinksSubscription(ILinks<ulong> links)
        {
            Name = "subscription_root";
            
            // Subscribe to link creation events
            Field<NonNullGraphType<LinksType>>(
                "link_created",
                arguments: new QueryArguments(
                    new QueryArgument<LinksBooleanExpressionInputType> { Name = "where" }
                ),
                resolve: context =>
                {
                    var whereFilter = context.GetArgument<LinksBooleanExpression>("where");
                    return _linkCreated.AsObservable()
                        .Where(link => FilterLink(link, whereFilter));
                });

            // Subscribe to link update events
            Field<NonNullGraphType<LinksType>>(
                "link_updated",
                arguments: new QueryArguments(
                    new QueryArgument<LinksBooleanExpressionInputType> { Name = "where" }
                ),
                resolve: context =>
                {
                    var whereFilter = context.GetArgument<LinksBooleanExpression>("where");
                    return _linkUpdated.AsObservable()
                        .Where(link => FilterLink(link, whereFilter));
                });

            // Subscribe to link deletion events
            Field<NonNullGraphType<LongGraphType>>(
                "link_deleted",
                arguments: new QueryArguments(
                    new QueryArgument<LongGraphType> { Name = "id" }
                ),
                resolve: context =>
                {
                    var idFilter = context.GetArgument<long?>("id");
                    return _linkDeleted.AsObservable()
                        .Where(linkId => idFilter == null || (long)linkId == idFilter)
                        .Select(linkId => (long)linkId);
                });

            // Subscribe to all link changes (created, updated, deleted)
            Field<NonNullGraphType<LinksType>>(
                "link_changed",
                arguments: new QueryArguments(
                    new QueryArgument<LinksBooleanExpressionInputType> { Name = "where" }
                ),
                resolve: context =>
                {
                    var whereFilter = context.GetArgument<LinksBooleanExpression>("where");
                    var created = _linkCreated.AsObservable().Where(link => FilterLink(link, whereFilter));
                    var updated = _linkUpdated.AsObservable().Where(link => FilterLink(link, whereFilter));
                    
                    return created.Merge(updated);
                });
        }

        private bool FilterLink(Links link, LinksBooleanExpression? whereFilter)
        {
            if (whereFilter == null)
                return true;

            // Apply basic filters
            if (whereFilter.id?._eq != null && link.id != whereFilter.id._eq)
                return false;

            if (whereFilter.from_id?._eq != null && (long)link.from_id != whereFilter.from_id._eq)
                return false;

            if (whereFilter.to_id?._eq != null && (long)link.to_id != whereFilter.to_id._eq)
                return false;

            // Note: type_id filtering would need the actual structure
            // For now, we'll skip it as LinksBooleanExpression.type_id is a complex object

            return true;
        }

        public void OnLinkCreated(Links link)
        {
            _linkCreated.OnNext(link);
        }

        public void OnLinkUpdated(Links link)
        {
            _linkUpdated.OnNext(link);
        }

        public void OnLinkDeleted(ulong linkId)
        {
            _linkDeleted.OnNext(linkId);
        }
    }
}
