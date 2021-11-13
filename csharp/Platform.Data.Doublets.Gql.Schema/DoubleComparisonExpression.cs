using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class DoubleComparisonExpression
    {
        public double? _eq;
        public double? _gt;
        public double? _gte;
        public List<double> _in;
        public bool? _is_null;
        public double? _lt;
        public double? _lte;
        public bool? _neq;
        public List<double> _nin;
    }
}
