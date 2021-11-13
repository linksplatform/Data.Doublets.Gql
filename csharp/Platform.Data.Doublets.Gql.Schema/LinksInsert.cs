namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksInsert
    {
        public MaterializedPathArrayRelationshipInsert _by_group;
        public MaterializedPathArrayRelationshipInsert _by_item;
        public MaterializedPathArrayRelationshipInsert _by_path_item;
        public MaterializedPathArrayRelationshipInsert _by_root;
        public BooleanExpressionObjectRelationshipInsert bool_exp;
        public LinksObjRelInsert from;
        public long? from_id;
        public long? id;
        public LinksArrayRelationshipInsert @in;
        public NumberObjectRelationshipInsert number;
        public LinksArrayRelationshipInsert @out;
        public StringObjectRelationshipInsert @string;
        public LinksObjRelInsert to;
        public long? to_id;
        public LinksObjRelInsert type;
        public long? type_id;
    }
}
