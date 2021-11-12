using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksArrayRelationshipInsert;

    /// <remarks>
    ///     """
    ///     input type for inserting array relation for remote table "links"
    ///     """
    ///     input links_arr_rel_insert_input {
    ///     data: [links_insert_input!]!
    ///     on_conflict: links_on_conflict
    ///     }
    /// </remarks>
    public class LinksArrayRelationshipInsertInputType : InputObjectGraphType<MappedType>
    {
        public LinksArrayRelationshipInsertInputType()
        {
            Name = "links_arr_rel_insert_input";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<LinksInsertInputType>>>>("data");
            Field<LinksOnConflictInputType>(nameof(MappedType.on_conflict));
        }
    }
}