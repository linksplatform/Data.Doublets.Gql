using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class LinksFieldsOrderByInputType : InputObjectGraphType<LinksFieldsOrderBy>
    {
        public LinksFieldsOrderByInputType()
        {
        }

        public LinksFieldsOrderByInputType(string name)
        {
            Name = name;
            Field(x => x.from_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.to_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.type_id, nullable: true, type: typeof(OrderByEnumType));
        }
    }
}
