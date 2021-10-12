using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LongComparisonExpression
    {
#nullable enable
        public long? _eq { get; set; }

        public long? _gt { get; set; }

        public long? _gte { get; set; }

        public List<long>? _in { get; set; }

        public bool? _is_null { get; set; }

        public long? _lt { get; set; }

        public long? _lte { get; set; }

        public long? _neq { get; set; }

        public List<long>? _nin { get; set; }
    }
}
