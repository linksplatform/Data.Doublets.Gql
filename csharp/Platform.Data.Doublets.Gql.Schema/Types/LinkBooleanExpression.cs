using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinkBooleanExpression
    {
        public List<LinkBooleanExpression> _and { get; set; }

        public List<LinkBooleanExpression> _or { get; set; }

        public LinkBooleanExpression _not { get; set; }

        public LinkBooleanExpression from { get; set; }

        public LongComparisonExpression from_id { get; set; }

        public LongComparisonExpression id { get; set; }

        public LongComparisonExpression to_id { get; set; }

        public LinkBooleanExpression @in { get; set; }

        public LinkBooleanExpression @type_id { get; set; }

        public LinkBooleanExpression @out { get; set; }

        public LinkBooleanExpression @to { get; set; }

        public LinkBooleanExpression @type { get; set; }

    }
}
