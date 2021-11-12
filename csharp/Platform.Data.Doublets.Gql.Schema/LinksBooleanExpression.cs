using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksBooleanExpression
    {
        public List<LinksBooleanExpression> _and { get; set; }
        public LinksBooleanExpression _by_group { get; set; }
        public LinksBooleanExpression _by_item { get; set; }
        public LinksBooleanExpression _by_path_item { get; set; }
        public LinksBooleanExpression _by_root { get; set; }
        public LinksBooleanExpression _not { get; set; }
        public List<LinksBooleanExpression> _or { get; set; }
        public List<LinksBooleanExpression> bool_exp { get; set; }
        public LinksBooleanExpression from { get; set; }
        public LongComparisonExpression from_id { get; set; }
        public LongComparisonExpression id { get; set; }
        public LinksBooleanExpression @in { get; set; }
        public LinksBooleanExpression number { get; set; }
        public LinksBooleanExpression @out { get; set; }
        public LinksBooleanExpression @string { get; set; }
        public LinksBooleanExpression to { get; set; }
        public LongComparisonExpression to_id { get; set; }
        public LinksBooleanExpression type { get; set; }
        public LinksBooleanExpression type_id { get; set; }
    }
}
