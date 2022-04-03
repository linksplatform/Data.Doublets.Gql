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
            var restrictionLink = new Link<ulong>(restriction);
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
                Variables = new GqlLink{ id = restrictionLink.Index != 0 ? (long)restrictionLink.Index : null , from_id = restrictionLink.Source != 0 ? (long)restrictionLink.Source : null, to_id = restrictionLink.Target != 0 ? (long)restrictionLink.Target : null}
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
            var restrictionLink = new Link<ulong>(restrictions);
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
                Variables = new GqlLink{ id = restrictionLink.Index != 0 ? (long)restrictionLink.Index : null , from_id = restrictionLink.Source != 0 ? (long)restrictionLink.Source : null, to_id = restrictionLink.Target != 0 ? (long)restrictionLink.Target : null}
            };
            var responseResult = _graphQlClient.SendQueryAsync<CountLinksResponseType>(personAndFilmsRequest).Result;
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
            var substitutionLink = new Link<ulong>(substitution);
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
                Variables = new GqlLink{ id = substitutionLink.Index != 0 ? (long)substitutionLink.Index : null , from_id = substitutionLink.Source != 0 ? (long)substitutionLink.Source : null, to_id = substitutionLink.Target != 0 ? (long)substitutionLink.Target : null}
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
            var restrictionLink = new Link<ulong>(restriction);
            var substitutionLink = new Link<ulong>(substitution);
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
                    id = (long?)(restrictionLink.Index != 0 ? (long)restrictionLink.Index : null),
                    from_id = (long?)(restrictionLink.Source != 0 ? (long)restrictionLink.Source : null),
                    to_id = (long?)(restrictionLink.Target != 0 ? (long)restrictionLink.Target : null),
                    substitution_from_id = (long?)(substitutionLink.Target != 0 ? (long)substitutionLink.Target : null),
                    substitution_to_id = (long?)(substitutionLink.Target != 0 ? (long)substitutionLink.Target : null)
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
            var restrictionLink = new Link<ulong>(restriction);
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
            var updateLinkRequest = new GraphQLRequest { Query = query, OperationName = "DeleteLink", Variables = (GqlLink)restrictionLink };
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
        public static implicit operator Link<ulong>(GqlLink gqlLink) => new((ulong)gqlLink.id.GetValueOrDefault(), (ulong)gqlLink.from_id.GetValueOrDefault(), (ulong)gqlLink.to_id.GetValueOrDefault());

        public static implicit operator GqlLink(Link<ulong> link) => new() { id = (long)link.Index, from_id = (long)link.Source, to_id = (long)link.Target };
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
