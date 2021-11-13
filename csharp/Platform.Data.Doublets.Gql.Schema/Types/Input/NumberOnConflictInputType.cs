using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = NumberOnConflict;

    /// <remarks>
    ///     """
    ///     on conflict condition type for table "number"
    ///     """
    ///     input number_on_conflict {
    ///     constraint: number_constraint!
    ///     update_columns: [number_update_column!]!
    ///     where: number_bool_exp
    ///     }
    /// </remarks>
    public class NumberOnConflictInputType : InputObjectGraphType<MappedType>
    {
        public NumberOnConflictInputType()
        {
            Name = "number_on_conflict";
            Field<NonNullGraphType<NumberConstraintEnumType>>(nameof(MappedType.constraint));
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<NumberUpdateColumnEnumType>>>>(
                nameof(MappedType.update_columns));
            Field(x => x.where, true, typeof(NumberBooleanExpressionInputType));
        }
    }
}
