using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// input type for inserting data into table "bool_exp"
    /// """
    /// input bool_exp_insert_input {
    ///   gql: String
    ///   id: bigint
    ///   link: links_arr_rel_insert_input
    ///   link_id: bigint
    ///   sql: String
    /// }
    /// </remarks>
    public class BooleanExpressionInsertInputType : InputObjectGraphType<BooleanExpressionInsert>
    {
        public BooleanExpressionInsertInputType()
        {
            Name = "bool_exp_insert_input";
            Field(x => x.gql, nullable: true, type: typeof(StringGraphType));
            Field(x => x.id, nullable: true, type: typeof(LongGraphType));
            Field(x => x.link, nullable: true, type: typeof(LinksArrayRelationshipInsertInputType));
            Field(x => x.link_id, nullable: true, type: typeof(LongGraphType));
            Field(x => x.sql, nullable: true, type: typeof(StringGraphType));
        }
    }
}
