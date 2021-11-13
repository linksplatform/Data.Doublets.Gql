using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class NumberBooleanExpression
    {
        public List<NumberBooleanExpression> _and;
        public NumberBooleanExpression _not;
        public List<NumberBooleanExpression> _or;
        public LongComparisonExpression id;
        public LinksBooleanExpression link;
        public LongComparisonExpression link_id;
        public DoubleComparisonExpression value;
    }
}
