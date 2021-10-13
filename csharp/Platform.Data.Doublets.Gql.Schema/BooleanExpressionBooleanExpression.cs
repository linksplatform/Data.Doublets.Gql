using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class BooleanExpressionBooleanExpression
    {
        public List<BooleanExpressionBooleanExpression?>? _and { get; set; }
        public BooleanExpressionBooleanExpression? _not { get; set; }
        public List<BooleanExpressionBooleanExpression?>? _or { get; set; }
        public StringComparisonExpression? gql { get; set; }
        public LongComparisonExpression? id { get; set; }
        public LinksBooleanExpression? link { get; set; }
        public LongComparisonExpression? link_id { get; set; }
        public StringComparisonExpression? sql { get; set; }
    }
}
