using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksBooleanExpression
    {
        public List<LinksBooleanExpression> _and;
        public LinksBooleanExpression _by_group;
        public LinksBooleanExpression _by_item;
        public LinksBooleanExpression _by_path_item;
        public LinksBooleanExpression _by_root;
        public LinksBooleanExpression _not;
        public List<LinksBooleanExpression> _or;
        public List<LinksBooleanExpression> bool_exp;
        public LinksBooleanExpression from;
        public LongComparisonExpression from_id;
        public LongComparisonExpression id;
        public LinksBooleanExpression @in;
        public LinksBooleanExpression number;
        public LinksBooleanExpression @out;
        public LinksBooleanExpression @string;
        public LinksBooleanExpression to;
        public LongComparisonExpression to_id;
        public LinksBooleanExpression type;
        public LinksBooleanExpression type_id;
    }
}
