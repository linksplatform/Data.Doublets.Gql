using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = NumberOnConflict;

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
