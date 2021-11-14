using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    using MappedType = LinksAggregateBigIntFields;

    public class LinksAggregateBigIntFieldsBaseType : ObjectGraphType<MappedType>
    {
        public LinksAggregateBigIntFieldsBaseType() { }

        public LinksAggregateBigIntFieldsBaseType(string name)
        {
            Name = name;
            Field<LongGraphType>(nameof(MappedType.id));
            Field<LongGraphType>(nameof(MappedType.from_id));
            Field<LongGraphType>(nameof(MappedType.to_id));
            Field<LongGraphType>(nameof(MappedType.type_id));
        }
    }
}
