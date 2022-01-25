using GraphQL;
using GraphQL.Client.Abstractions;
using Platform.Collections;
using Platform.Threading;

namespace Platform.Data.Doublets.Gql.Client
{
    public class LinksGqlAdapter<TLink> : ILinks<TLink>
    {
        private readonly IEqualityComparer<TLink> _equalityComparer = EqualityComparer<TLink>.Default;
        private readonly IGraphQLClient _graphQlClient;

        public LinksGqlAdapter(IGraphQLClient graphQlClient, LinksConstants<TLink> constants)
        {
            _graphQlClient = graphQlClient;
            Constants = constants;
        }

        public LinksConstants<TLink> Constants { get; }

        public TLink Count(IList<TLink>? restriction)
        {
            var countRequest = new GraphQLRequest
            {
                Query = @"
                        query CountLinks ($id: Long, $from_id: Long, $to_id: Long) {
                          links(where: { id: {_eq: $id}, from_id: {_eq: $from_id}, to_id: {_eq: $to_id} }) {
                            id
                          }
                        }",
                OperationName = "CountLinks",
                Variables = new { id = restriction[Constants.IndexPart], from_id = restriction[Constants.SourcePart], to_id = restriction[Constants.TargetPart] }
            };
            var responseResult = _graphQlClient.SendQueryAsync<CountLinksResponseType>(countRequest).Result;
            if (responseResult.Errors?.Length > 0)
            {
                foreach (var error in responseResult.Errors)
                {
                    throw new Exception(error.Message);
                }
            }
            return (TLink)(object)Convert.ToUInt64(responseResult.Data.links.Count);
        }

        public TLink Each(IList<TLink>? restrictions, ReadHandler<TLink>? handler)
        {
            var personAndFilmsRequest = new GraphQLRequest
            {
                Query = @"
                        query GetLinks ($id: Long, $from_id: Long, $to_id: Long) {
                          links(where: { id: {_eq: $id}, from_id: {_eq: $from_id}, to_id: {_eq: $to_id} }) {
                            id
                            from_id
                            to_id
                          }
                        }",
                OperationName = "GetLinks",
                Variables = new { id = restrictions[Constants.IndexPart], from_id = restrictions[Constants.SourcePart], to_id = restrictions[Constants.TargetPart] }
            };
            var responseResult = _graphQlClient.SendQueryAsync<GetLinksResponseType>(personAndFilmsRequest).Result;
            if (responseResult.Errors?.Length > 0)
            {
                foreach (var error in responseResult.Errors)
                {
                    throw new Exception(error.Message);
                }
            }
            foreach (var link in responseResult.Data.links)
            {
                var handlerResult = handler(new Link<TLink>(link.id, link.from_id, link.to_id));
                if (_equalityComparer.Equals(Constants.Break, handlerResult))
                {
                    return handlerResult;
                }
            }
            return Constants.Continue;
        }

        public TLink Create(IList<TLink>? restrictions, WriteHandler<TLink>? handler)
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
            var id = responseResult.Data.insert_links_one.id;
            var fromId = responseResult.Data.insert_links_one.from_id;
            var toId = responseResult.Data.insert_links_one.to_id;
            handler(Link<TLink>.Null, new Link<TLink>(id, fromId, toId));
            return responseResult.Data.insert_links_one.id;
        }

        public TLink Update(IList<TLink>? restrictions, IList<TLink>? substitution, WriteHandler<TLink>? handler)
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
            return handler(restrictions, substitution);
        }

        public TLink Create(IList<TLink>? restrictions)
        {
            TLink result = default;
            Create(restrictions, (before, after) =>
            {
                result = after[Constants.IndexPart];
                return Constants.Continue;
            });
            return result;
        }

        public TLink Update(IList<TLink>? restrictions, IList<TLink>? substitution)
        {
            TLink result = default;
            Update(restrictions, substitution, (_, after) =>
            {
                result = after[Constants.IndexPart];
                return Constants.Continue;
            });
            return result;
        }

        public TLink Delete(IList<TLink>? restrictions)
        {
            var index = restrictions[0];
            var source = restrictions[1];
            var target = restrictions[2];
            var isIndexNull = _equalityComparer.Equals(index, Constants.Null);
            var isSourceNull = _equalityComparer.Equals(source, Constants.Null);
            var isTargetNull = _equalityComparer.Equals(target, Constants.Null);
            var isSourceAny = _equalityComparer.Equals(target, Constants.Any);
            var isTargetAny = _equalityComparer.Equals(target, Constants.Any);
            string query;
            if (!isIndexNull)
            {
                query = @"
                        mutation DeleteLinkWithSourceAndTarget ($id: Long!){
                          delete_links(where: {id: { _eq: $id } }) {
                            returning {
                              id
                            }
                          }
                        }";
            }
            else if (!isSourceNull && !isTargetNull && !isSourceAny && !isTargetAny)
            {
                query = @"
                        mutation DeleteLinkWithSourceAndTarget ($from_id: Long!, $to_id: Long!){
                          delete_links(where: { from_id: { _eq: $from_id }, to_id: { _eq: $to_id } }) {
                            returning {
                              id
                            }
                          }
                        }";
            }
            else
            {
                throw new NotImplementedException();
            }
            var updateLinkRequest = new GraphQLRequest { Query = query, OperationName = "DeleteLink", Variables = new { id = index, from_id = source, to_id = target } };
            var responseResult = _graphQlClient.SendMutationAsync<DeleteResponseType>(updateLinkRequest).AwaitResult();
            if (!responseResult.Errors.IsNullOrEmpty())
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            // Use returning[0] cause right now it is not possible to return multiple links
            return responseResult.Data.delete_links.returning[0].id;
        }

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

        public struct CountLinksResponseType
        {
            public List<Link> links { get; set; }
        }

        public struct GetLinksResponseType
        {
            public List<Link> links { get; set; }
        }

        public struct Link
        {
            public TLink id { get; set; }

            public TLink from_id { get; set; }

            public TLink to_id { get; set; }
        }
    }
}
