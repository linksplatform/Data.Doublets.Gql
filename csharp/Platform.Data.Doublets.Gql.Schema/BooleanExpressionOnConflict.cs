using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class BooleanExpressionOnConflict
    {
        public BoolExpressionConstraint constraint { get; set; }
        public List<BooleanExpressionOnConflict> update_columns { get; set; }
        public BooleanExpressionBooleanExpression where { get; set; }
    }
}