using GraphQL;
using GraphQL.Client.Abstractions;
using Newtonsoft.Json.Linq;
using Platform.Collections;
using Platform.Data.Doublets.Gql.Schema;
using Platform.Data.Doublets.Gql.Schema.Types;
using Platform.Threading;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Platform.Data.Doublets.Gql.Client
{
    public class LinksGqlAdapter<TLink> : ILinks<TLink>
    {
        public IEqualityComparer<TLink> EqualityComparer = EqualityComparer<TLink>.Default;
        private IGraphQLClient _graphQlClient;

        public LinksGqlAdapter(IGraphQLClient graphQlClient, LinksConstants<TLink> linksConstants)
        {
            _graphQlClient = graphQlClient;
            Constants = linksConstants;
        }

        public TLink Count(IList<TLink> restriction)
        {
            var personAndFilmsRequest = new GraphQLRequest {
                Query =@"
                        query CountLinks ($id: Long, $from_id: Long, $to_id: Long) {
                          links(where: { id: {_eq: $id}, from_id: {_eq: $from_id}, to_id: {_eq: $to_id} }) {
                            id
                          }
                        }",
                OperationName = "CountLinks",
                Variables = new {
                    id = restriction[Constants.IndexPart],
                    from_id = restriction[Constants.SourcePart],
                    to_id = restriction[Constants.TargetPart]
                }
            };
            var responseResult = _graphQlClient.SendQueryAsync<CountResponseType>(personAndFilmsRequest).Result;
            if (responseResult.Errors?.Length > 0)
            {
                foreach (var error in responseResult.Errors)
                {
                    throw new Exception(error.Message);
                }
            }
            return (TLink)(object)responseResult.Data.links.Count;
        }

        public TLink Each(Func<IList<TLink>, TLink> handler, IList<TLink> restrictions) => throw new NotImplementedException();

        public TLink Create(IList<TLink> restrictions)
        {
            var createLinkRequest = new GraphQLRequest
            {
                Query = @"
                        mutation CreateLink ($from_id: Long!, $to_id: Long!) {
                          insert_links_one(object: {from_id: $from_id, to_id: $to_id}) {
                            id
                          }
                        }",
                OperationName = "CreateLink",
                Variables = new { from_id = restrictions[0], to_id = restrictions[1] }
            };
            var responseResult = _graphQlClient.SendMutationAsync<CreateResponseType>(createLinkRequest).AwaitResult();
            if (!responseResult.Errors.IsNullOrEmpty())
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            return responseResult.Data.insert_links_one.id;
        }

        public TLink Update(IList<TLink> restrictions, IList<TLink> substitution)
        {
            var updateLinkRequest = new GraphQLRequest
            {
                Query = @"
                        mutation UpdateLink ($from_id: Long!, $to_id: Long!, $substitution_from_id: Long!, $substitution_to_id: Long!) {
                          update_links(_set: { from_id: $substitution_from_id, to_id: $substitution_to_id }, where: { from_id: { _eq: $from_id }, to_id: { _eq: $to_id } }) {
                            returning {
                              id
                            }
                          }
                        }",
                OperationName = "UpdateLink",
                Variables = new { from_id = restrictions[0], to_id = restrictions[1], substitution_from_id = substitution[0], substitution_to_id = substitution[1] }
            };
            var responseResult = _graphQlClient.SendMutationAsync<CreateResponseType>(updateLinkRequest).AwaitResult();
            if (!responseResult.Errors.IsNullOrEmpty())
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            return responseResult.Data.insert_links_one.id;
        }

        public void Delete(IList<TLink> restrictions)
        {
            var updateLinkRequest = new GraphQLRequest
            {
                Query = @"
                        mutation DeleteLink ($from_id: Long!, $to_id: Long!){
                          delete_links(where: { from_id: { _eq: $from_id }, to_id: { _eq: $to_id } }) {
                            returning {
                              id
                            }
                          }
                        }",
                OperationName = "DeleteLink",
                Variables = new { from_id = restrictions[0], to_id = restrictions[1]}
            };
            var responseResult = _graphQlClient.SendMutationAsync<DeleteResponseType>(updateLinkRequest).AwaitResult();
            if (!responseResult.Errors.IsNullOrEmpty())
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
        }

        public LinksConstants<TLink> Constants { get; }
        public struct CreateResponseType
        {
            public Link insert_links_one { get; set; }
        }
        public struct DeleteResponseType
        {
            public DeleteLinks delete_links { get; set; }
            public struct DeleteLinks
            {
                public List<Link> returning { get; set; }
            }
        }

        public struct CountResponseType
        {
            public List<Link> links { get; set; }
        }
        public struct Link
        {
            public TLink id { get; set; }
        }
    }
}
