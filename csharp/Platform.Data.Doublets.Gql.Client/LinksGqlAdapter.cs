using GraphQL;
using GraphQL.Client.Abstractions;
using Platform.Collections.Lists;
using Platform.Delegates;
using Platform.Threading;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Data.Doublets.Gql.Client
{
    public class LinksGqlAdapter<TLinkAddress> : ILinks<TLinkAddress> where TLinkAddress : struct
    {
        private readonly IEqualityComparer<TLinkAddress> _equalityComparer = EqualityComparer<TLinkAddress>.Default;
        private readonly IGraphQLClient _graphQlClient;

        public LinksGqlAdapter(IGraphQLClient graphQlClient, LinksConstants<TLinkAddress> constants)
        {
            _graphQlClient = graphQlClient;
            Constants = constants;
        }

        public LinksConstants<TLinkAddress> Constants { get; }

        public TLinkAddress Count(IList<TLinkAddress>? restriction)
        {
            var restrictionLink = new Link<TLinkAddress>(restriction);
            TLinkAddress? index = _equalityComparer.Equals(default, restrictionLink.Index) ? null : restrictionLink.Index;
            TLinkAddress? source = _equalityComparer.Equals(default, restrictionLink.Source) ? null : restrictionLink.Source;
            TLinkAddress? target = _equalityComparer.Equals(default, restrictionLink.Target) ? null : restrictionLink.Target;
            var countRequest = new GraphQLRequest
            {
                Query = (1 == restriction.Count)
                ? @"
                        query CountLinks ($id: Long) {
                          links(where: { id: {_eq: $id} }) {
                            id
                          }
                        }"
                :
                @"
                        query CountLinks ($from_id: Long, $to_id: Long) {
                          links(where: { from_id: {_eq: $from_id}, to_id: {_eq: $to_id} }) {
                            id
                          }
                        }",
                OperationName = "CountLinks",
                Variables = new { id = index, from_id = source, to_id = target }
            };
            var responseResult = _graphQlClient.SendQueryAsync<CountLinksResponseType<TLinkAddress>>(countRequest).Result;
            if (responseResult.Errors != null)
            {
                foreach (var error in responseResult.Errors)
                {
                    throw new Exception(error.Message);
                }
            }
            return (TLinkAddress)(object)Convert.ToUInt64(responseResult.Data.links.Count);
        }

        public TLinkAddress Each(IList<TLinkAddress>? restrictions, ReadHandler<TLinkAddress>? handler)
        {
            var restrictionLink = new Link<TLinkAddress>(restrictions);
            TLinkAddress? index = _equalityComparer.Equals(default, restrictionLink.Index) ? null : restrictionLink.Index;
            TLinkAddress? source = _equalityComparer.Equals(default, restrictionLink.Source) ? null : restrictionLink.Source;
            TLinkAddress? target = _equalityComparer.Equals(default, restrictionLink.Target) ? null : restrictionLink.Target;
            var personAndFilmsRequest = new GraphQLRequest
            {
                Query = (1 == restrictions?.Count) ? @"
                        query GetLinks ($id: Long) {
                          links(where: { id: {_eq: $id} }) {
                            id,
                            from_id,
                            to_id
                          }
                        }"
                :
                @"query GetLinks ($from_id: Long, $to_id: Long) {
                          links(where: { from_id: {_eq: $from_id}, to_id: {_eq: $to_id} }) {
                            id,
                            from_id,
                            to_id
                          }
                        }",
                OperationName = "GetLinks",
                Variables = new GqlLink<TLinkAddress?>{ id = index, from_id = source, to_id = target}
            };
            var responseResult = _graphQlClient.SendQueryAsync<CountLinksResponseType<TLinkAddress>>(personAndFilmsRequest).Result;
            if (responseResult.Errors != null)
            {
                foreach (var error in responseResult.Errors)
                {
                    throw new Exception(error.Message);
                }
            }
            for (var i = 0; i < responseResult.Data.links.Count; i++)
            {
                var link = responseResult.Data.links[i];
                var handlerResult = handler?.Invoke(new Link<TLinkAddress>(link.id, link.from_id, link.to_id)) ?? Constants.Continue;
                if (_equalityComparer.Equals(Constants.Break, handlerResult))
                {
                    return handlerResult;
                }
            }
            return Constants.Continue;
        }


        public TLinkAddress Create(IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? handler)
        {
            var substitutionLink = new Link<TLinkAddress>(substitution);
            var createLinkRequest = new GraphQLRequest
            {
                Query = @"
                        mutation CreateLink ($from_id: Long!, $to_id: Long!) {
                          insert_links_one(object: {from_id: $from_id, to_id: $to_id}) {
                              id,
                              from_id
                              to_id
                          }
                        }",
                OperationName = "CreateLink",
                Variables = (GqlLink<TLinkAddress>)substitutionLink
            };
            var responseResult = _graphQlClient.SendMutationAsync<CreateResponseType<TLinkAddress>>(createLinkRequest).AwaitResult();
            if (responseResult.Errors != null)
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            return handler?.Invoke(null, (Link<TLinkAddress>)responseResult.Data.insert_links_one) ?? Constants.Continue;
        }

        public TLinkAddress Update(IList<TLinkAddress>? restriction, IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? handler)
        {
            var restrictionLink = new Link<TLinkAddress>(restriction);
            var substitutionLink = new Link<TLinkAddress>(substitution);
            var query = (1 == restriction?.Count)
                ? @"
                        mutation UpdateLink ($id: Long!, $substitution_from_id: Long!, $substitution_to_id: Long!) {
                          update_links(_set: { from_id: $substitution_from_id, to_id: $substitution_to_id }, where: { id: { _eq: $id } }) {
                            returning {
                              id,
                              from_id
                              to_id
                            }
                          }
                        }"
                :
                @"
                        mutation UpdateLink ($from_id: Long!, $to_id: Long!, $substitution_from_id: Long!, $substitution_to_id: Long!) {
                          update_links(_set: { from_id: $substitution_from_id, to_id: $substitution_to_id }, where: { from_id: { _eq: $from_id }, to_id: { _eq: $to_id } }) {
                            returning {
                              id,
                              from_id
                              to_id
                            }
                          }
                        }";
            var updateLinkRequest = new GraphQLRequest
            {
                Query = query,
                OperationName = "UpdateLink",
                Variables = new
                {
                    id = restrictionLink.Index,
                    from_id = restrictionLink.Source,
                    to_id = restrictionLink.Target,
                    substitution_from_id = substitutionLink.Source,
                    substitution_to_id = substitutionLink.Target
                }
            };
            var responseResult = _graphQlClient.SendMutationAsync<UpdateResponseType<TLinkAddress>>(updateLinkRequest).AwaitResult();
            if (responseResult.Errors != null)
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            return handler?.Invoke(restriction, (Link<TLinkAddress>)responseResult.Data.update_links.returning.First()) ?? Constants.Continue;
        }

        public TLinkAddress Delete(IList<TLinkAddress>? restriction, WriteHandler<TLinkAddress>? handler)
        {
            var restrictionLink = new Link<TLinkAddress>(restriction);
            var query = (1 == restriction?.Count)
                ? @"
                        mutation DeleteLink ($id: Long!){
                          delete_links(where: {id: { _eq: $id } }) {
                            returning {
                              id,
                              from_id
                              to_id
                            }
                          }
                        }"
                :
                @"
                        mutation DeleteLink ($from_id: Long!, $to_id: Long!){
                          delete_links(where: { from_id: { _eq: $from_id }, to_id: { _eq: $to_id } }) {
                            returning {
                              id,
                              from_id
                              to_id
                            }
                          }
                        }";
            var updateLinkRequest = new GraphQLRequest { Query = query, OperationName = "DeleteLink", Variables = (GqlLink<TLinkAddress>)restrictionLink };
            var responseResult = _graphQlClient.SendMutationAsync<DeleteResponseType<TLinkAddress>>(updateLinkRequest).AwaitResult();
            if (responseResult.Errors != null)
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            // Use returning[0] cause right now it is not possible to return multiple links
            return handler?.Invoke((Link<TLinkAddress>)responseResult.Data.delete_links.returning[0], null) ?? Constants.Continue;
        }
    }
    public struct GqlLink<TLinkAddress>
    {
        public TLinkAddress id;
        public TLinkAddress from_id;
        public TLinkAddress to_id;
        public static implicit operator Link<TLinkAddress>(GqlLink<TLinkAddress> gqlLink) => new(gqlLink.id, gqlLink.from_id, gqlLink.to_id);

        public static implicit operator GqlLink<TLinkAddress>(Link<TLinkAddress> link) => new() { id = link.Index, from_id = link.Source, to_id = link.Target };
    }

    public struct CreateResponseType<TLinkAddress>
    {
        public GqlLink<TLinkAddress> insert_links_one { get; set; }
    }

    public struct UpdateResponseType<TLinkAddress>
    {
        public UpdateLinks update_links { get; set; }

        public struct UpdateLinks
        {
            public List<GqlLink<TLinkAddress>> returning { get; set; }
        }
    }

    public struct DeleteResponseType<TLinkAddress>
    {
        public DeleteLinks delete_links { get; set; }

        public struct DeleteLinks
        {
            public List<GqlLink<TLinkAddress>> returning { get; set; }
        }
    }

    public struct CountLinksResponseType<TLinkAddress>
    {
        public List<GqlLink<TLinkAddress>> links { get; set; }
    }

    public struct GetLinksResponseType<TLinkAddress>
    {
        public List<GqlLink<TLinkAddress>> links { get; set; }
    }
}
