using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// on conflict condition type for table "mp"
    /// """
    /// input mp_on_conflict {
    ///   constraint: mp_constraint!
    ///   update_columns: [mp_update_column!]!
    ///   where: mp_bool_exp
    /// }
    /// </remarks>
    public class MaterializedPathOnConflictInputType : InputObjectGraphType<MaterializedPathOnConflict>
    {
        public MaterializedPathOnConflictInputType()
        {
            Name = "mp_on_conflict";
            Description = "on conflict condition type for table \"mp\"";
            Field<NonNullGraphType<MaterializedPathConstraintEnumType>>("constraint");
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<MaterializedPathUpdateColumnEnumType>>>>("update_columns");
            Field(x => x.where, nullable: true, type: typeof(MaterializedPathBooleanExpressionInputType));
        }
    }
}
