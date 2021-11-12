using System.Collections.Generic;
using System.Linq;
using GraphQL;
using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Input;

namespace Platform.Data.Doublets.Gql.Schema
{
    /// <remarks>
    ///     """mutation root"""
    ///     type mutation_root {
    ///     """
    ///     delete data from the table: "links"
    ///     """
    ///     delete_links(
    ///     """filter the rows which have to be deleted"""
    ///     where: links_bool_exp!
    ///     ): links_mutation_response
    ///     """
    ///     delete single row from the table: "links"
    ///     """
    ///     delete_links_by_pk(id: bigint!): links
    ///     """
    ///     insert data into the table: "links"
    ///     """
    ///     insert_links(
    ///     """the rows to be inserted"""
    ///     objects: [links_insert_input!]!
    ///     """on conflict condition"""
    ///     on_conflict: links_on_conflict
    ///     ): links_mutation_response
    ///     """
    ///     insert a single row into the table: "links"
    ///     """
    ///     insert_links_one(
    ///     """the row to be inserted"""
    ///     object: links_insert_input!
    ///     """on conflict condition"""
    ///     on_conflict: links_on_conflict
    ///     ): links
    ///     """
    ///     update data of the table: "links"
    ///     """
    ///     update_links(
    ///     """increments the integer columns with given value of the filtered values"""
    ///     _inc: links_inc_input
    ///     """sets the columns of the filtered rows to the given values"""
    ///     _set: links_set_input
    ///     """filter the rows which have to be updated"""
    ///     where: links_bool_exp!
    ///     ): links_mutation_response
    ///     """
    ///     update single row of the table: "links"
    ///     """
    ///     update_links_by_pk(
    ///     """increments the integer columns with given value of the filtered values"""
    ///     _inc: links_inc_input
    ///     """sets the columns of the filtered rows to the given values"""
    ///     _set: links_set_input
    ///     pk_columns: links_pk_columns_input!
    ///     ): links
    ///     }
    /// </remarks>
    public class LinksMutation : ObjectGraphType<object>
    {
        public LinksMutation(ILinks<ulong> links)
        {
            Name = "mutation_root";
            Field<LinksMutationResponseType>("delete_links",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LinksBooleanExpressionInputType>> { Name = "where" }
                ),
                resolve: context =>
                {
                    var response = new LinksMutationResponse
                    {
                        returning = LinksQuery.GetLinks(context, links).ToList()
                    };
                    response.affected_rows = response.returning.Count;
                    foreach (var linkToDelete in response.returning) links.Delete((ulong)linkToDelete.id);
                    return response;
                });
            Field<LinksType>("delete_links_by_pk",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<LongGraphType>> { Name = "id" }
                },
                resolve: context => "");
            Field<LinksMutationResponseType>("insert_links",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListGraphType<NonNullGraphType<LinksInsertInputType>>>>
                        { Name = "objects" },
                    new QueryArgument<LinksOnConflictInputType> { Name = "on_conflict" }
                ),
                resolve: context =>
                {
                    var response = new LinksMutationResponse { returning = new List<Links>() };
                    foreach (var link in context.GetArgument<List<LinksInsert>>("objects"))
                        response.returning.Add(InsertLink(links, link));
                    response.affected_rows = response.returning.Count;
                    return response;
                });
            Field<LinksType>("insert_links_one",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LinksInsertInputType>> { Name = "object" },
                    new QueryArgument<LinksOnConflictInputType> { Name = "on_conflict" }
                ),
                resolve: context =>
                {
                    return new LinksMutationResponse
                    {
                        returning = new List<Links> { InsertLink(links, context.GetArgument<LinksInsert>("object")) },
                        affected_rows = 1
                    };
                });
            Field<LinksMutationResponseType>("update_links",
                arguments: new QueryArguments(
                    new QueryArgument<LinksIncInputType> { Name = "_inc" },
                    new QueryArgument<LinksSetInputType> { Name = "_set" },
                    new QueryArgument<NonNullGraphType<LinksBooleanExpressionInputType>> { Name = "where" }
                ),
                resolve: context =>
                {
                    var set = context.GetArgument<LinksSet>("_set");
                    var response = new LinksMutationResponse { returning = new List<Links>() };
                    foreach (var link in LinksQuery.GetLinks(context, links))
                    {
                        var updatedLink = links.UpdateOrCreateOrGet((ulong)link.from_id, (ulong)link.to_id,
                            (ulong)set.from_id, (ulong)set.to_id);
                        response.returning.Add(new Links(links.GetLink(updatedLink)));
                    }

                    response.affected_rows = response.returning.Count;
                    return response;
                });
            Field<LinksType>("update_links_by_pk",
                arguments: new QueryArguments(
                    new QueryArgument<LinksIncInputType> { Name = "_inc" },
                    new QueryArgument<LinksSetInputType> { Name = "_set" },
                    new QueryArgument<NonNullGraphType<LinksPkColumnsInputType>> { Name = "pk_columns" }
                ),
                resolve: context => ""
            );
        }

        public static Links InsertLink(object service, Links links)
        {
            var link = (ILinks<ulong>)service;
            var create = link.GetOrCreate((ulong)links.from_id, (ulong)links.to_id);
            return LinksType.GetLinkOrDefault(service, (long)create);
        }
        public static Links InsertLink(object service, LinksInsert linksInsert)
        {
            var link = (ILinks<ulong>)service;
            var create = link.GetOrCreate((ulong)linksInsert.from_id, (ulong)linksInsert.to_id);
            return LinksType.GetLinkOrDefault(service, (long)create);
        }
    }
}
