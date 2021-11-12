using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class DoubleComparisonExpression
    {
        public double? _eq { get; set; }
        public double? _gt { get; set; }
        public double? _gte { get; set; }
        public List<double> _in { get; set; }
        public bool? _is_null { get; set; }
        public double? _lt { get; set; }
        public double? _lte { get; set; }
        public bool? _neq { get; set; }
        public List<double> _nin { get; set; }
    }
}