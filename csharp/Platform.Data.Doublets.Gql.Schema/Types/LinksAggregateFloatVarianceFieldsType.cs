using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """aggregate variance on columns"""
    /// type links_variance_fields {
    ///   from_id: Float
    ///   id: Float
    ///   to_id: Float
    ///   type_id: Float
    /// }
    /// </remarks>
    public class LinksAggregateFloatVarianceFieldsType : LinksAggregateFloatFieldsType
    {
        public LinksAggregateFloatVarianceFieldsType()
        {
            Name = "links_variance_fields";
            Field(x => x.id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.from_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.to_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.type_id, nullable: true, type: typeof(FloatGraphType));
        }
    }
}