using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = BooleanExpressionOnConflict;
    /// <remarks>
    /// """
    /// on conflict condition type for table "bool_exp"
    /// """
    /// input bool_exp_on_conflict {
    ///   constraint: bool_exp_constraint!
    ///   update_columns: [bool_exp_update_column!]!
    ///   where: bool_exp_bool_exp
    /// } 
    /// </remarks>
    public class BooleanExpressionOnConflictInputType : InputObjectGraphType<MappedType>
    {
        public BooleanExpressionOnConflictInputType()
        {
            Name = "bool_exp_on_conflict";
            Field<BooleanExpressionOnConflictInputType>(nameof(MappedType.constraint));
            Field<ListGraphType<NonNullGraphType<BooleanExpressionUpdateColumnEnumType>>>(
                nameof(MappedType.update_columns));
            Field(x => x.where, nullable: true, type: typeof(BooleanExpressionBooleanExpressionInputType));
        }
    }
}
