using GraphQL;
using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// ordering options when selecting data from "links"
    /// """
    /// input links_order_by {
    ///   from: links_order_by
    ///   from_id: order_by
    ///   id: order_by
    ///   in_aggregate: links_aggregate_order_by
    ///   out_aggregate: links_aggregate_order_by
    ///   to: links_order_by
    ///   to_id: order_by
    ///   type: links_order_by
    ///   type_id: order_by
    /// }
    /// </remarks>
    internal class LinksOrderByInputType : InputObjectGraphType<LinksOrderBy>
    {
        public LinksOrderByInputType()
        {
            Field<LinksOrderByInputType>("from");
            Field(x => x.from_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.id, nullable:true, type: typeof(OrderByEnumType));
            Field<LinksOrderByInputType>("to");
            Field(x => x.to_id, nullable: true, type: typeof(OrderByEnumType));
            Field<LinksOrderByInputType>("type");
            Field(x => x.type_id, nullable: true, type: typeof(OrderByEnumType));
            Field<LinksAggregateOrderByInputType>("out_aggregate");
            Field<LinksAggregateOrderByInputType>("in_aggregate");
        }
    }
}