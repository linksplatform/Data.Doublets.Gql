using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class BooleanExpressionBooleanExpression
    {
        public List<BooleanExpressionBooleanExpression> _and;
        public BooleanExpressionBooleanExpression _not;
        public List<BooleanExpressionBooleanExpression> _or;
        public StringComparisonExpression gql;
        public LongComparisonExpression id;
        public LinksBooleanExpression link;
        public LongComparisonExpression link_id;
        public StringComparisonExpression sql;
    }
}
