using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class MaterializedPathOnConflictInputType : InputObjectGraphType<MaterializedPathOnConflict>
    {
        public MaterializedPathOnConflictInputType()
        {
            Name = "mp_on_conflict";
            Description = "on conflict condition type for table \"mp\"";
            Field<NonNullGraphType<MaterializedPathConstraintEnumType>>("constraint");
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<MaterializedPathUpdateColumnEnumType>>>>(
                "update_columns");
            Field(x => x.where, true, typeof(MaterializedPathBooleanExpressionInputType));
        }
    }
}