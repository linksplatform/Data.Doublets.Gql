using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Input;


namespace Platform.Data.Doublets.Gql.Schema.Types
{
    internal class LinksFieldsOrderByType : InputObjectGraphType<LinksFieldsOrderBy>
    {
        public LinksFieldsOrderByType()
        {
            Field(x => x.id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.from_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.to_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.type_id, nullable: true, type: typeof(OrderByEnumType));
        }
    }
}
