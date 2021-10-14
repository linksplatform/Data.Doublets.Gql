using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = StringOnConflict;
    /// <remarks>
    /// """
    /// on conflict condition type for table "string"
    /// """
    /// input string_on_conflict {
    ///   constraint: string_constraint!
    ///   update_columns: [string_update_column!]!
    ///   where: string_bool_exp
    /// }
    /// </remarks>
    public class StringOnConflictInputType : InputObjectGraphType<MappedType>
    {
        public StringOnConflictInputType()
        {
            Name = "String_on_conflict";
            Field<StringConstraintEnumType>(nameof(MappedType.constraint));
            Field<ListGraphType<NonNullGraphType<StringUpdateColumnEnumType>>>(nameof(MappedType.update_columns));
            Field(x => x.where, nullable: true, type: typeof(StringBooleanExpressionInputType));
        }
    }
}
