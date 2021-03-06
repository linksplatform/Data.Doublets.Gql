using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = MaterializedPathOnConflict;

    public class MaterializedPathOnConflictInputType : InputObjectGraphType<MappedType>
    {
        public MaterializedPathOnConflictInputType()
        {
            Name = "mp_on_conflict";
            Description = "on conflict condition type for table \"mp\"";
            Field<NonNullGraphType<MaterializedPathConstraintEnumType>>("constraint");
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<MaterializedPathUpdateColumnEnumType>>>>("update_columns");
            Field<MaterializedPathBooleanExpressionInputType>(nameof(MappedType.where));
        }
    }
}
