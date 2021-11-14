using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinksArrRelInsertInputType : InputObjectGraphType<LinksArrayRelationshipInsert>
    {
        public LinksArrRelInsertInputType()
        {
            Field<ListGraphType<LinksInsertInputType>>("data");
            Field(x => x.on_conflict, true, typeof(LinksOnConflictInputType));
        }
    }
}