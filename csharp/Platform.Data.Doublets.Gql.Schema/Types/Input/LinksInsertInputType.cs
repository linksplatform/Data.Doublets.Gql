using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksInsert;
    public class LinksInsertInputType : InputObjectGraphType<MappedType>
    {
        public LinksInsertInputType()
        {
            Name = "links_insert_input";
            Description = "input type for inserting data into table \"links\"";
            Field<MaterializedPathArrayRelationshipInsertInputType>(nameof(MappedType._by_group));
            Field<MaterializedPathArrayRelationshipInsertInputType>(nameof(MappedType._by_item));
            Field<MaterializedPathArrayRelationshipInsertInputType>(nameof(MappedType._by_path_item));
            Field<MaterializedPathArrayRelationshipInsertInputType>(nameof(MappedType._by_root));
            Field<BooleanExpressionObjectRelationshipInsertInputType>(nameof(MappedType.bool_exp));
            Field<LinksObjRelInsertInputType>(nameof(MappedType.from));
            Field<LongGraphType>(nameof(MappedType.from_id));
            Field<LongGraphType>(nameof(MappedType.id));
            Field<LinksArrayRelationshipInsertInputType>(nameof(MappedType.@in));
            Field<NumberObjectRelationshipInsertInputType>(nameof(MappedType.number));
            Field<LinksArrayRelationshipInsertInputType>(nameof(MappedType.@out));
            Field<StringObjectRelationshipInsertInputType>(nameof(MappedType.@string));
            Field<LinksObjRelInsertInputType>(nameof(MappedType.to));
            Field<LongGraphType>(nameof(MappedType.to_id));
            Field<LinksObjRelInsertInputType>(nameof(MappedType.type));
            Field<LongGraphType>(nameof(MappedType.type_id));
        }
    }
}