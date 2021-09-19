using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """aggregate avg on columns"""
    /// type links_avg_fields {
    ///   from_id: Float
    ///   id: Float
    ///   to_id: Float
    ///   type_id: Float
    /// }
    /// </remarks>
    public class LinksAggregateFloatAvgFieldsType : LinksAggregateFloatFieldsType
    {
        public LinksAggregateFloatAvgFieldsType()
        {
            Name = "links_avg_fields";
            Field(x => x.id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.from_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.to_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.type_id, nullable: true, type: typeof(FloatGraphType));
        }
    }
}