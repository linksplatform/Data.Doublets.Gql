using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class NumberBooleanExpression
    {
        public List<NumberBooleanExpression> _and { get; set; }
        public NumberBooleanExpression _not { get; set; }
        public List<NumberBooleanExpression> _or { get; set; }
        public LongComparisonExpression id { get; set; }
        public LinksBooleanExpression link { get; set; }
        public LongComparisonExpression link_id { get; set; }
        public DoubleComparisonExpression value { get; set; }
    }
}
