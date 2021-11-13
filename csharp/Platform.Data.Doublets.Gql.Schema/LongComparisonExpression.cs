using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LongComparisonExpression
    {
        public long? _eq;

        public long? _gt;

        public long? _gte;

        public List<long> _in;

        public bool? _is_null;

        public long? _lt;

        public long? _lte;

        public long? _neq;

        public List<long> _nin;
    }
}
