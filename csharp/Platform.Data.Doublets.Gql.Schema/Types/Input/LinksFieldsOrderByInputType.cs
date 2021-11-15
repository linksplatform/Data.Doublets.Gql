using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksFieldsOrderBy;

    public class LinksFieldsOrderByInputType : InputObjectGraphType<MappedType>
    {
        public LinksFieldsOrderByInputType()
        {
        }

        public LinksFieldsOrderByInputType(string name)
        {
            Name = name;
            Field<OrderByEnumType>(nameof(MappedType.from_id));
            Field<OrderByEnumType>(nameof(MappedType.id));
            Field<OrderByEnumType>(nameof(MappedType.to_id));
            Field<OrderByEnumType>(nameof(MappedType.type_id));
        }
    }
}
