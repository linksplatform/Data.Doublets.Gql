using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksOnConflict;

    /// <remarks>
    ///     """
    ///     on conflict condition type for table "links"
    ///     """
    ///     input links_on_conflict {
    ///     constraint: links_constraint!
    ///     update_columns: [links_update_column!]!
    ///     where: links_bool_exp
    ///     }
    /// </remarks>
    public class LinksOnConflictInputType : InputObjectGraphType<MappedType>
    {
        public LinksOnConflictInputType()
        {
            Name = "links_on_conflict";
            Field<NonNullGraphType<LinksConstraintEnumType>>(nameof(MappedType.constraint));
            Field<NonNullGraphType<ListGraphType<LinksUpdateColumnEnumType>>>(nameof(MappedType.update_columns));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.where));
        }
    }
}
