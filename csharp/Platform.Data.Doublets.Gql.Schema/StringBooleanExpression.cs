using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class StringBooleanExpression
    {
        public List<StringBooleanExpression> _and;
        public StringBooleanExpression _not;
        public List<StringBooleanExpression> _or;
        public LongComparisonExpression id;
        public LinksBooleanExpression link;
        public LongComparisonExpression link_id;
        public StringComparisonExpression value;
    }
}
