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
    public class LinksGqlAdapter : ILinks<ulong>
    {
        private readonly IEqualityComparer<ulong> _equalityComparer = EqualityComparer<ulong>.Default;
        private readonly IGraphQLClient _graphQlClient;

        public LinksGqlAdapter(IGraphQLClient graphQlClient, LinksConstants<ulong> constants)
        {
            _graphQlClient = graphQlClient;
            Constants = constants;
        }

        public LinksConstants<ulong> Constants { get; }

        public ulong Count(IList<ulong>? restriction)
        {
            var restrictionLink = new GqlLink(restriction);
            var countRequest = new GraphQLRequest
            {
                Query = @"
                        query CountLinks ($id: Long, $from_id: Long, $to_id: Long) {
                          links(where: { id: {_eq: $id}, from_id: {_eq: $from_id}, to_id: {_eq: $to_id} }) {
                            id
                          }
                        }",
                OperationName = "CountLinks",
                Variables = restrictionLink
            };
            var responseResult = _graphQlClient.SendQueryAsync<CountLinksResponseType>(countRequest).Result;
            if (responseResult.Errors != null)
            {
                foreach (var error in responseResult.Errors)
                {
                    throw new Exception(error.Message);
                }
            }
            return (ulong)Convert.ToUInt64(responseResult.Data.links.Count);
        }

        public ulong Each(IList<ulong>? restrictions, ReadHandler<ulong>? handler)
        {
            var restrictionLink = new GqlLink(restrictions);
            var personAndFilmsRequest = new GraphQLRequest
            {
                Query = @"
                        query GetLinks ($from_id: Long, $to_id: Long) {
                          links(where: { from_id: {_eq: $from_id}, to_id: {_eq: $to_id} }) {
                            id,
                            from_id,
                            to_id
                          }
                        }",
                OperationName = "GetLinks",
                Variables = restrictionLink
            };
            var responseResult = _graphQlClient.SendQueryAsync<CountLinksResponseType>(personAndFilmsRequest).Result;
            if (responseResult.Errors != null)
            {
                foreach (var error in responseResult.Errors)
                {
                    throw new Exception(error.Message);
                }
            }
            foreach (var link in responseResult.Data.links)
            {
                var handlerResult = handler?.Invoke((Link<ulong>?)(link)) ?? Constants.Continue;
                if (_equalityComparer.Equals(Constants.Break, handlerResult))
                {
                    return handlerResult;
                }
            }
            return Constants.Continue;
        }


        public ulong Create(IList<ulong>? substitution, WriteHandler<ulong>? handler)
        {
            long? source;
            long? target;
            if (substitution == null)
            {
                source = 0;
                target = 0;
            }
            else if (substitution.Count == 2)
            {
                source = (long)substitution[0];
                target = (long)substitution[1];
            }
            else if (substitution.Count == 3)
            {
                source = (long)substitution[1];
                target = (long)substitution[2];
            }
            else
            {
                throw new ArgumentException("Substitution is invalid.");
            }
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
                Variables = new { from_id = source, to_id = target}
            };
            var responseResult = _graphQlClient.SendMutationAsync<CreateResponseType>(createLinkRequest).AwaitResult();
            if (responseResult.Errors != null)
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            return handler?.Invoke(null, (Link<ulong>?)responseResult.Data.insert_links_one) ?? Constants.Continue;
        }

        public ulong Update(IList<ulong>? restriction, IList<ulong>? substitution, WriteHandler<ulong>? handler)
        {
            var restrictionLink = new GqlLink(restriction);
            var substitutionLink = new GqlLink(substitution);
            var query = @"
                        mutation UpdateLink ($id: Long, $from_id: Long, $to_id: Long, $substitution_from_id: Long, $substitution_to_id: Long) {
                          update_links(_set: { id: $id, from_id: $substitution_from_id, to_id: $substitution_to_id }, where: { id: { _eq: $id }, from_id: { _eq: $from_id }, to_id: { _eq: $to_id } }) {
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
                    id = restrictionLink.id,
                    from_id = restrictionLink.from_id,
                    to_id = restrictionLink.to_id,
                    substitution_from_id = substitutionLink.from_id,
                    substitution_to_id = substitutionLink.to_id
                }
            };
            var responseResult = _graphQlClient.SendMutationAsync<UpdateResponseType>(updateLinkRequest).AwaitResult();
                if (responseResult.Errors != null)
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            return handler?.Invoke(restriction, (Link<ulong>?)(responseResult.Data.update_links.returning.FirstOrDefault())) ?? Constants.Continue;
        }

        public ulong Delete(IList<ulong>? restriction, WriteHandler<ulong>? handler)
        {
            var restrictionLink = new GqlLink(restriction);
            var query = @"
                        mutation DeleteLink ($id: Long, $substitution_from_id: Long, $substitution_to_id: Long) {
                          delete_links(where: { id: { _eq: $id } from_id: { _eq: $substitution_from_id }, to_id: { _eq: $substitution_to_id } }) {
                            returning {
                              id,
                              from_id
                              to_id
                            }
                          }
                        }";
            var updateLinkRequest = new GraphQLRequest { Query = query, OperationName = "DeleteLink", Variables = restrictionLink };
            var responseResult = _graphQlClient.SendMutationAsync<DeleteResponseType>(updateLinkRequest).AwaitResult();
            if (responseResult.Errors != null)
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            // Use returning[0] cause right now it is not possible to return multiple links
            return handler?.Invoke((Link<ulong>?)responseResult.Data.delete_links.returning[0], null) ?? Constants.Continue;
        }
    }
    public struct GqlLink
    {
        public long? id;
        public long? from_id;
        public long? to_id;

        public GqlLink(params ulong[]? restriction) : this((IList<ulong>?)restriction) {}

        public GqlLink(IList<ulong>? link)
        {
            if (link == null || link.Count == 0)
            {
                id = null;
                from_id = null;
                to_id = null;
            }
            else if (link.Count == 1)
            {
                id = (long)link[0];
                from_id = null;
                to_id = null;
            }
            else if (link.Count == 2)
            {
                id = null;
                from_id = (long)link[0];
                to_id = (long)link[1];
            }
            else if (link.Count == 3)
            {
                id = (long)link[0];
                from_id = (long)link[1];
                to_id = (long)link[2];
            }
            else
            {
                throw new ArgumentException("Invalid restriction");
            }
        }
        public static implicit operator Link<ulong>(GqlLink gqlLink) => new Link<ulong>((ulong)gqlLink.id.GetValueOrDefault(), (ulong)gqlLink.from_id.GetValueOrDefault(), (ulong)gqlLink.to_id.GetValueOrDefault());

        public static implicit operator GqlLink(Link<ulong> link) => new GqlLink(link.Index, link.Source, link.Target);
    }

    public struct CreateResponseType
    {
        public GqlLink insert_links_one { get; set; }
    }

    public struct UpdateResponseType
    {
        public UpdateLinks update_links { get; set; }

        public struct UpdateLinks
        {
            public List<GqlLink> returning { get; set; }
        }
    }

    public struct DeleteResponseType
    {
        public DeleteLinks delete_links { get; set; }

        public struct DeleteLinks
        {
            public List<GqlLink> returning { get; set; }
        }
    }

    public struct CountLinksResponseType
    {
        public List<GqlLink> links { get; set; }
    }

    public struct GetLinksResponseType
    {
        public List<GqlLink> links { get; set; }
    }
}
