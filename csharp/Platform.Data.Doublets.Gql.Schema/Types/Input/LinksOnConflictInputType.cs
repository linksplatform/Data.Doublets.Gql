using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
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
    public class LinksOnConflictInputType : InputObjectGraphType<LinksOnConflict>
    {
        public LinksOnConflictInputType()
        {
            Name = "links_on_conflict";
            Field<LinksConstraintEnumType>("constraint");
            Field<ListGraphType<LinksUpdateColumnEnumType>>("update_columns");
            Field(x => x.where, true, typeof(LinksBooleanExpressionInputType));
        }
    }
}