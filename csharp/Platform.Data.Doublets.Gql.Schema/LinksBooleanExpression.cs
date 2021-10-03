using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksBooleanExpression
    {
        public List<LinksBooleanExpression> _and { get; set; }

        public List<LinksBooleanExpression> _or { get; set; }

        public LinksBooleanExpression _not { get; set; }

        public LinksBooleanExpression from { get; set; }

        public LongComparisonExpression from_id { get; set; }

        public LongComparisonExpression id { get; set; }

        public LongComparisonExpression to_id { get; set; }

        public LinksBooleanExpression @in { get; set; }

        public LinksBooleanExpression @type_id { get; set; }

        public LinksBooleanExpression @out { get; set; }

        public LinksBooleanExpression @to { get; set; }

        public LinksBooleanExpression @type { get; set; }

    }
}
