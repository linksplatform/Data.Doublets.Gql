using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = BooleanExpressionOnConflict;

    public class BooleanExpressionOnConflictInputType : InputObjectGraphType<MappedType>
    {
        public BooleanExpressionOnConflictInputType()
        {
            Name = "bool_exp_on_conflict";
            Description = "on conflict condition type for table \"bool_exp\"";
            Field<NonNullGraphType<BooleanExpressionConstraintEnumType>>(nameof(MappedType.constraint));
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<BooleanExpressionUpdateColumnEnumType>>>>(nameof(MappedType.update_columns));
            Field<BooleanExpressionBooleanExpressionInputType>(nameof(MappedType.where));
        }
    }
}
