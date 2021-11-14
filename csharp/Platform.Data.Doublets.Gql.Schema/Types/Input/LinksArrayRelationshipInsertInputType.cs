using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksArrayRelationshipInsert;
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