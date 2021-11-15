using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinksArrRelInsertInputType : InputObjectGraphType<LinksArrayRelationshipInsert>
    {
        public LinksArrRelInsertInputType()
        {
            Field<ListGraphType<LinksInsertInputType>>("data");
            Field<LinksOnConflictInputType>(nameof(MappedType.on_conflict));
        }
    }
}
