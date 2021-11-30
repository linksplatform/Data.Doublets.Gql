namespace Platform.Data.Doublets.Gql.Schema
{
    public class MaterializedPathInsert
    {
        public LinksObjRelInsert by_group { get; set; }

        public MaterializedPathArrayRelationshipInsert by_item { get; set; }

        public MaterializedPathArrayRelationshipInsert by_path_item { get; set; }

        public MaterializedPathArrayRelationshipInsert by_position { get; set; }

        public MaterializedPathArrayRelationshipInsert by_root { get; set; }

        public long? group_id { get; set; }

        public long? id { get; set; }

        public string insert_category { get; set; }

        public LinksObjRelInsert item { get; set; }

        public long? item_id { get; set; }

        public LinksObjRelInsert path_item { get; set; }

        public long? path_item_depth { get; set; }

        public long? path_item_id { get; set; }

        public string position_id { get; set; }

        public LinksObjRelInsert root { get; set; }

        public long? root_id { get; set; }
    }
}
