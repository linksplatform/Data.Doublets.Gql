using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using Platform.Collections.Lists;
using Platform.Delegates;
using Platform.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace Platform.Data.Doublets.Gql.Client
{

    public class DeepGqlAdapter : ILinks<ulong>
    {
        private readonly IEqualityComparer<ulong> _equalityComparer = EqualityComparer<ulong>.Default;
        private readonly IGraphQLClient _graphQlClient;
        public readonly ulong DoubletTypeId;
        public static readonly string Token = "";

        public DeepGqlAdapter(IGraphQLClient graphQlClient, LinksConstants<ulong> constants)
        {
            _graphQlClient = graphQlClient;
            Constants = constants;
            DoubletTypeId = GetDoubletTypeOrDefault();
            if (_equalityComparer.Equals(DoubletTypeId, default))
            {
                DoubletTypeId = CreateDoubletType();
            }
        }

        public LinksConstants<ulong> Constants { get; }

        public ulong Count(IList<ulong>? restriction)
        {
            var restrictionLink = new GqlLink(restriction);
            string query;
            object? variables= null;
            if (restriction == null || restriction.Count == 0)
            {
                query = @"
                        query CountLinks($type_id: bigint!)  {
                          links(where: {type_id: {_eq: $type_id} }) {
                            id
                          }
                        }";
                variables = new { type_id = DoubletTypeId };
            }
            else if (restriction.Count is 2 or 3)
            {
                query = @"
                        query CountLinks ($from_id: bigint, $to_id: bigint, $type_id: bigint!)  {
                          links(where: { from_id: {_eq: $from_id}, to_id: {_eq: $to_id}, type_id: {_eq: $type_id} }) {
                            id
                          }
                        }";
                variables = new { from_id = restrictionLink.from_id, to_id = restrictionLink.to_id, type_id = DoubletTypeId };
            }
            else
            {
                throw new ArgumentException("Invalid restriction");
            }
            var countRequest = new DeepGqlRequest
            {
                Query = query,
                OperationName = "CountLinks",
                Variables = variables
            };
            var responseResult = _graphQlClient.SendQueryAsync<ReadLinksResponseType>(countRequest).Result;
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
            string query;
            object? variables = null;
            if (restrictions == null || restrictions.Count == 0)
            {
                query = @"
                        query GetLinks($type_id: bigint!)  {
                          links(where: {type_id: {_eq: $type_id} }) {
                            id,
                            from_id,
                            to_id
                          }
                        }";
                variables = new { type_id = DoubletTypeId };
            }
            else if (restrictions.Count is 2 or 3)
            {
                query = @"
                        query GetLinks ($from_id: bigint, $to_id: bigint, $type_id: bigint!) {
                          links(where: { from_id: {_eq: $from_id}, to_id: {_eq: $to_id}, type_id: {_eq: $type_id} }) {
                            id,
                            from_id,
                            to_id
                          }
                        }";
                variables = new { from_id = restrictionLink.from_id, to_id = restrictionLink.to_id, type_id = DoubletTypeId };
            }
            else
            {
                throw new ArgumentException("Invalid restriction");
            }
            var personAndFilmsRequest = new DeepGqlRequest
            {
                Query = query,
                OperationName = "GetLinks",
                Variables = variables
            };
            var responseResult = _graphQlClient.SendQueryAsync<ReadLinksResponseType>(personAndFilmsRequest).Result;
            if (responseResult.Errors != null)
            {
                foreach (var error in responseResult.Errors)
                {
                    throw new Exception(error.Message);
                }
            }
            if (responseResult.Data.links == null)
            {
                return Constants.Continue;
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
            var substitutionLink = new GqlLink(substitution);
            string query;
            object? variables = null;
            if (substitution == null || substitution.Count == 0)
            {
                query = @"
                        mutation CreateLink($type_id: bigint!) {
                          insert_links_one(object: {from_id: 0, to_id: 0, type_id: $type_id }) {
                              id,
                              from_id
                              to_id
                          }
                        }";
                variables = new { type_id = DoubletTypeId };
            }
            else if (substitution.Count == 2 || substitution.Count == 3)
            {
                query = @"
                        mutation CreateLink ($from_id: bigint, $to_id: bigint, $type_id: bigint!) {
                          insert_links_one(object: {from_id: $from_id, to_id: $to_id, type_id: $type_id }) {
                              id,
                              from_id
                              to_id
                          }
                        }";
                variables = new { from_id = substitutionLink.from_id, to_id = substitutionLink.to_id, type_id = DoubletTypeId };
            }
            else
            {
                throw new ArgumentException("Invalid substitution");
            }
            var createLinkRequest = new DeepGqlRequest
            {
                Query = query,
                OperationName = "CreateLink",
                Variables = variables
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
            string query;
            object? variables = null;
            if(restriction == null || restriction.Count == 0)
            {
                throw new ArgumentException("Invalid restriction");
            }
            else if (restriction.Count == 1)
            {
                query = @"
                        mutation UpdateLink ($id: bigint, $substitution_from_id: bigint, $substitution_to_id: bigint, $type_id: bigint!) {
                          update_links(_set: { from_id: $substitution_from_id, to_id: $substitution_to_id }, where: { id: { _eq: $id }, type_id: { _eq: $type_id } }) {
                            returning {
                              id,
                              from_id
                              to_id
                            }
                          }
                        }";
                variables = new { id = restrictionLink.id, substitution_from_id = substitutionLink.from_id, substitution_to_id = substitutionLink.to_id, type_id = DoubletTypeId };
            }
            else if (restriction.Count is 2 or 3)
            {
                query = @"
                        mutation UpdateLink ($from_id: bigint, $to_id: bigint, $substitution_from_id: bigint, $substitution_to_id: bigint, $type_id: bigint!) {
                          update_links(_set: { from_id: $substitution_from_id, to_id: $substitution_to_id }, where: {  from_id: { _eq: $from_id }, to_id: { _eq: $to_id }, type_id: { _eq: $type_id } }) {
                            returning {
                              id,
                              from_id
                              to_id
                            }
                          }
                        }";
                variables = new { from_id = restrictionLink.from_id, to_id = restrictionLink.to_id , substitution_from_id = substitutionLink.from_id, substitution_to_id = substitutionLink.to_id, type_id = DoubletTypeId };

            }
            else
            {
                throw new ArgumentException("Invalid restriction");
            }
            var updateLinkRequest = new DeepGqlRequest
            {
                Query = query,
                OperationName = "UpdateLink",
                Variables = variables
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
            string query;
            object? variables;
            if (restriction == null || restriction.Count == 0)
            {
                throw new ArgumentException("Invalid restriction");
            }
            else if (restriction.Count == 1)
            {
                query = @"
                        mutation DeleteLink ($id: bigint, $type_id: bigint!) {
                          delete_links(where: { id: { _eq: $id }, type_id: { _eq: $type_id } }) {
                            returning {
                              id,
                              from_id
                              to_id
                            }
                          }
                        }";
                variables = new { id = restrictionLink.id, type_id = DoubletTypeId };
            }
            else if (restriction.Count == 2 || restriction.Count == 3)
            {
                query = @"
                        mutation DeleteLink ($from_id: bigint, $to_id: bigint, $type_id: bigint!) {
                          delete_links(where: { from_id: { _eq: $from_id }, to_id: { _eq: $to_id }, type_id: { _eq: $type_id } }) {
                            returning {
                              id,
                              from_id
                              to_id
                            }
                          }
                        }";
                variables = new { from_id = restrictionLink.from_id, to_id = restrictionLink.to_id };
            }
            else
            {
                throw new ArgumentException("Invalid restriction");
            }
            var updateLinkRequest = new DeepGqlRequest { Query = query, OperationName = "DeleteLink", Variables = variables };
            var responseResult = _graphQlClient.SendMutationAsync<DeleteResponseType>(updateLinkRequest).AwaitResult();
            if (responseResult.Errors != null)
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            foreach (var link in responseResult.Data.delete_links.returning)
            {
                var handlerResult = handler?.Invoke((Link<ulong>?)(link), null) ?? Constants.Continue;
                if (_equalityComparer.Equals(Constants.Break, handlerResult))
                {
                    return handlerResult;
                }
            }
            return Constants.Continue;
        }

        private ulong GetDoubletTypeOrDefault()
        {
            var getRequest = new DeepGqlRequest { Query = @"
query GetDoubletType {
  links(where: {string: {value: {_eq: ""doublet""}}, type_id: {_eq: ""1""}}) {
    id
  }
}",
                OperationName = "GetDoubletType" };
            var responseResult = _graphQlClient.SendMutationAsync<ReadLinksResponseType>(getRequest).AwaitResult();
            if (responseResult.Errors != null)
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            return (ulong)(responseResult.Data.links?.GetElementOrDefault(0).id ?? 0);
        }

        private ulong CreateDoubletType()
        {
            var createRequest = new DeepGqlRequest { Query = @"
mutation CreateDoubletType {
  insert_links_one(object: {type_id: ""1"", string: {data: {value: ""doublet""}}}) {
    id
  }
}",
                OperationName = "CreateDoubletType"};
            var responseResult = _graphQlClient.SendMutationAsync<CreateResponseType>(createRequest).AwaitResult();
            if (responseResult.Errors != null)
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            return (ulong)responseResult.Data.insert_links_one.id!;
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

    public struct ReadLinksResponseType
    {
        public List<GqlLink>? links { get; set; }
    }

    public struct GetLinksResponseType
    {
        public List<GqlLink> links { get; set; }
    }

    public class DeepGqlRequest : GraphQLHttpRequest
    {
        public override HttpRequestMessage ToHttpRequestMessage(GraphQLHttpClientOptions options, IGraphQLJsonSerializer serializer)
        {
            var request = base.ToHttpRequestMessage(options, serializer);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            return request;
        }
    }
    }

}
