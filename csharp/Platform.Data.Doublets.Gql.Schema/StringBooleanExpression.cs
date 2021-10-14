using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class StringBooleanExpression
    {
        public List<StringBooleanExpression?>? _and { get; set; }
        public StringBooleanExpression? _not { get; set; }
        public List<StringBooleanExpression?>? _or { get; set; }
        public LongComparisonExpression? id { get; set; }
        public LinksBooleanExpression? link { get; set; }
        public LongComparisonExpression? link_id { get; set; }
        public StringComparisonExpression? value { get; set; }
    }
}
