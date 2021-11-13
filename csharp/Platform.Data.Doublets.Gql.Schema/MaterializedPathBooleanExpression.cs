using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class MaterializedPathBooleanExpression
    {
        public List<MaterializedPathBooleanExpression> _and;
        public MaterializedPathBooleanExpression _not;
        public List<MaterializedPathBooleanExpression> _or;
        public LinksBooleanExpression by_group;
        public MaterializedPathBooleanExpression by_item;
        public MaterializedPathBooleanExpression by_path_item;
        public MaterializedPathBooleanExpression by_position;
        public MaterializedPathBooleanExpression by_root;
        public LongComparisonExpression group_id;
        public LongComparisonExpression id;
        public StringComparisonExpression insert_category;
        public LinksBooleanExpression item;
        public LongComparisonExpression item_id;
        public LinksBooleanExpression path_item;
        public LongComparisonExpression path_item_depth;
        public LongComparisonExpression path_item_id;
        public StringComparisonExpression position_id;
        public LinksBooleanExpression root;
        public LongComparisonExpression root_id;
    }
}
