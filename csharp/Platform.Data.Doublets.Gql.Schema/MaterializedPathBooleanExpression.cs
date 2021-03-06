using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class MaterializedPathBooleanExpression
    {
        public List<MaterializedPathBooleanExpression> _and { get; set; }

        public MaterializedPathBooleanExpression _not { get; set; }

        public List<MaterializedPathBooleanExpression> _or { get; set; }

        public LinksBooleanExpression by_group { get; set; }

        public MaterializedPathBooleanExpression by_item { get; set; }

        public MaterializedPathBooleanExpression by_path_item { get; set; }

        public MaterializedPathBooleanExpression by_position { get; set; }

        public MaterializedPathBooleanExpression by_root { get; set; }

        public LongComparisonExpression group_id { get; set; }

        public LongComparisonExpression id { get; set; }

        public StringComparisonExpression insert_category { get; set; }

        public LinksBooleanExpression item { get; set; }

        public LongComparisonExpression item_id { get; set; }

        public LinksBooleanExpression path_item { get; set; }

        public LongComparisonExpression path_item_depth { get; set; }

        public LongComparisonExpression path_item_id { get; set; }

        public StringComparisonExpression position_id { get; set; }

        public LinksBooleanExpression root { get; set; }

        public LongComparisonExpression root_id { get; set; }
    }
}
