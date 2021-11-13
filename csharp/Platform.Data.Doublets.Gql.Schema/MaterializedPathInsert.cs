namespace Platform.Data.Doublets.Gql.Schema
{
    public class MaterializedPathInsert
    {
        public LinksObjRelInsert by_group;
        public MaterializedPathArrayRelationshipInsert by_item;
        public MaterializedPathArrayRelationshipInsert by_path_item;
        public MaterializedPathArrayRelationshipInsert by_position;
        public MaterializedPathArrayRelationshipInsert by_root;
        public long? group_id;
        public long? id;
        public string insert_category;
        public LinksObjRelInsert item;
        public long? item_id;
        public LinksObjRelInsert path_item;
        public long? path_item_depth;
        public long? path_item_id;
        public string position_id;
        public LinksObjRelInsert root;
        public long? root_id;
    }
}
