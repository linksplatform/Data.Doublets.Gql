using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class BooleanExpressionOnConflict
    {
        public BoolExpressionConstraint constraint;
        public List<BooleanExpressionOnConflict> update_columns;
        public BooleanExpressionBooleanExpression where;
    }
}
