using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = StringOnConflict;

    /// <remarks>
    ///     """
    ///     on conflict condition type for table "string"
    ///     """
    ///     input string_on_conflict {
    ///     constraint: string_constraint!
    ///     update_columns: [string_update_column!]!
    ///     where: string_bool_exp
    ///     }
    /// </remarks>
    public class StringOnConflictInputType : InputObjectGraphType<MappedType>
    {
        public StringOnConflictInputType()
        {
            Name = "string_on_conflict";
            Description = "on conflict condition type for table \"string\"";
            Field<NonNullGraphType<StringConstraintEnumType>>(nameof(MappedType.constraint));
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<StringUpdateColumnEnumType>>>>(
                nameof(MappedType.update_columns));
            Field(x => x.where, true, typeof(StringBooleanExpressionInputType));
        }
    }
}
