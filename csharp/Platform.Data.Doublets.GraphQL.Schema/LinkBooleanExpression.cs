using System.Collections.Generic;

namespace Gql.Samples.Schemas.Link
{
    public class LinkBooleanExpression
    {
        public List<LinkBooleanExpression> _and{ get; set; }

        public List<LinkBooleanExpression> _or{ get; set; }

        public LinkBooleanExpression _not{ get; set; }

        public LinkBooleanExpression from{ get; set; }

        public LongComparisonExpression from_id{ get; set; }

        public LongComparisonExpression id{ get; set; }

        public LinkBooleanExpression @in { get; set; }

        public LinkBooleanExpression @out { get; set; }

        public LinkBooleanExpression to{ get; set; }

        public LongComparisonExpression to_id{ get; set; }

        public LinkBooleanExpression type{ get; set; }

        public LongComparisonExpression type_id{ get; set; }
    }
}
