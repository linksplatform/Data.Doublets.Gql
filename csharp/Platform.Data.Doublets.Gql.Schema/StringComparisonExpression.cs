using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class StringComparisonExpression
    {
        public string _eq;
        public string _gt;
        public string _gte;
        public string _ilike;
        public List<string> _in;
        public bool? _is_null;
        public string _like;
        public string _lt;
        public string _lte;
        public string _neq;
        public string _nilike;
        public List<string> _nin;
        public string _nlike;
        public string _nsimilar;
        public string _similar;
    }
}
