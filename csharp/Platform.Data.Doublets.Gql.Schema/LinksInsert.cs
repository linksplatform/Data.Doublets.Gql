using Platform.Data.Doublets.Gql.Schema.Types.Input;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksInsert
    {
        public MaterializedPathArrayRelationshipInsert? _by_group { get; set; }
        public MaterializedPathArrayRelationshipInsert? _by_item { get; set; }
        public MaterializedPathArrayRelationshipInsert? _by_path_item { get; set; }
        public MaterializedPathArrayRelationshipInsert? _by_root { get; set; }
        public BooleanExpressionObjectRelationshipInsert? bool_exp { get; set; }
        public LinksObjRelInsert? from { get; set; }
        public long? from_id { get; set; }
        public long? id { get; set; }
        public LinksArrayRelationshipInsert? @in { get; set; }
        public NumberObjectRelationshipInsert? number { get; set; }
        public LinksArrayRelationshipInsert? @out { get; set; }
        public StringObjectRelationshipInsert? @string { get; set; }
        public LinksObjRelInsert? to { get; set; }
        public long? to_id { get; set; }
        public LinksObjRelInsert? type { get; set; }
        public long? type_id { get; set; }
    }
}
