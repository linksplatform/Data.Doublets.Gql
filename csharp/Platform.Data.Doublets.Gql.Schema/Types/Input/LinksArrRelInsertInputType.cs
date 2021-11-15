using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksArrayRelationshipInsert;

    public class LinksArrRelInsertInputType : InputObjectGraphType<MappedType>
    {
        public LinksArrRelInsertInputType()
        {
            Field<ListGraphType<LinksInsertInputType>>("data");
            Field<LinksOnConflictInputType>(nameof(MappedType.on_conflict));
        }
    }
}
