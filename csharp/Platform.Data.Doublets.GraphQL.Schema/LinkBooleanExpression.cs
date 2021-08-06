using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Samples.Schemas.Link;

namespace GraphQL.Samples.Schemas.Link
{
    public class LinkBooleanExpression
    {
        public List<LinkBooleanExpression> _and;

        public List<LinkBooleanExpression> _or;

        public LinkBooleanExpression _not;

        public LinkBooleanExpression from;

        public LongComparisonExpression from_id;

        public LongComparisonExpression id;

        public LinkBooleanExpression In;

        public LinkBooleanExpression Out;

        public LinkBooleanExpression to;

        public LongComparisonExpression to_id;

        public LinkBooleanExpression type;

        public LongComparisonExpression type_id;
    }
}
