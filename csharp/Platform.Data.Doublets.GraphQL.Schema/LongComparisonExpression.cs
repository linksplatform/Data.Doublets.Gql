using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Link
{
    public class LongComparisonExpression
    {
        public long _eq;

        public long _gt;

        public long _gte;

        public List<long> _in;

        public bool is_null;

        public long _lt;

        public long _lte;

        public long _neq;

        public List<long> _nin;
    }
}

