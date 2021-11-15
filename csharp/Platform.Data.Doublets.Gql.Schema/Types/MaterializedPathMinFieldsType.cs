using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    using MappedType = MaterializedPathMinFields;
    public class MaterializedPathMinFieldsType : ObjectGraphType<MappedType>
    {
        public MaterializedPathMinFieldsType()
        {
            Name = "mp_min_fields";
            Description = "aggregate min on columns";
            Field<LongGraphType>(nameof(MappedType.group_id));
            Field<LongGraphType>(nameof(MappedType.id));
            Field<StringGraphType>(nameof(MappedType.insert_category));
            Field<LongGraphType>(nameof(MappedType.item_id));
            Field<LongGraphType>(nameof(MappedType.path_item_depth));
            Field<LongGraphType>(nameof(MappedType.path_item_id));
            Field<StringGraphType>(nameof(MappedType.position_id));
            Field<LongGraphType>(nameof(MappedType.root_id));
        }
    }
}
