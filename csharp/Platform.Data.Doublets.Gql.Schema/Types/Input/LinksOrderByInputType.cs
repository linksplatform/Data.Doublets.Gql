using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksOrderBy;

    /// <remarks>
    ///     """
    ///     ordering options when selecting data from "links"
    ///     """
    ///     input links_order_by {
    ///     from: links_order_by
    ///     from_id: order_by
    ///     id: order_by
    ///     in_aggregate: links_aggregate_order_by
    ///     out_aggregate: links_aggregate_order_by
    ///     to: links_order_by
    ///     to_id: order_by
    ///     type: links_order_by
    ///     type_id: order_by
    ///     }
    /// </remarks>
    public class LinksOrderByInputType : InputObjectGraphType<MappedType>
    {
        public LinksOrderByInputType()
        {
            Name = "links_order_by";
            Field(x => x.from, nullable: true, type: typeof(LinksOrderByInputType));
            Field(x => x.from_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.in_aggregate, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.out_aggregate, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.to, nullable: true, type: typeof(LinksOrderByInputType));
            Field(x => x.to_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.type, nullable: true, type: typeof(LinksOrderByInputType));
            Field(x => x.type_id, nullable: true, type: typeof(OrderByEnumType));
        }
    }
}
