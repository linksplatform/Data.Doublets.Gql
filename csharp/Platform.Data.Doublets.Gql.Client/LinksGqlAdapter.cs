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
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;

namespace Platform.Data.Doublets.Gql.Client
{

    public class LinksGqlAdapter : ILinks<ulong>
    {
        private static readonly IEqualityComparer<ulong> _equalityComparer = EqualityComparer<ulong>.Default;
        private readonly IGraphQLClient _graphQlClient;

        public LinksGqlAdapter(IGraphQLClient graphQlClient, LinksConstants<ulong> constants)
        {
            _graphQlClient = graphQlClient;
            Constants = constants;
        }

        public LinksConstants<ulong> Constants { get; }

        public ulong Count(IList<ulong>? restriction)
        {
            var restrictionLink = new GqlLink(restriction, Constants.Any); 
            var countRequest = new GraphQLRequest
            {
                Query = $@"
                        query CountLinks {{
                          links(where: {{ {restrictionLink.ToGqlWhereArguments()} }}) {{
                            id
                          }}
                        }}
                        ",
                OperationName = "CountLinks",
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
            var restrictionLink = new GqlLink(restrictions, Constants.Any); 
            var personAndFilmsRequest = new GraphQLRequest
            {
                Query = $@"
                        query GetLinks {{
                          links(where: {{ {restrictionLink.ToGqlWhereArguments()}}}) {{
                            id,
                            from_id,
                            to_id
                          }}
                        }}
                        ",
                OperationName = "GetLinks",
            };
            var responseResult = _graphQlClient.SendQueryAsync<ReadLinksResponseType>(personAndFilmsRequest).Result;
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
            var substitutionLink = new GqlLink(substitution, Constants.Any); 
            var createLinkRequest = new GraphQLRequest
            {
                Query = $@"
                        mutation CreateLink {{
                          insert_links_one(object: {{ {substitutionLink.ToGqlObjectArguments()}}}) {{
                              id,
                              from_id
                              to_id
                          }}
                        }}
                        ",
                OperationName = "CreateLink",
            };
            var responseResult = _graphQlClient.SendMutationAsync<CreateResponseType>(createLinkRequest).AwaitResult();
            if (responseResult.Errors != null)
            {
                foreach (var responseResultError in responseResult.Errors)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            return handler?.Invoke(null, (Link<ulong>?)responseResult.Data.insert_links_one) ?? Constants.Continue;
        }

        public ulong Update(IList<ulong>? restriction, IList<ulong>? substitution, WriteHandler<ulong>? handler)
        {
            var restrictionLink = new GqlLink(restriction, Constants.Any); 
            var substitutionLink = new GqlLink(substitution, Constants.Any); 
            var updateLinkRequest = new GraphQLRequest
            {
                Query = $@"
                        mutation UpdateLink {{
                          update_links(_set: {{ from_id: {substitutionLink.from_id}, to_id: {substitutionLink.to_id} }}, where: {{  {restrictionLink.ToGqlWhereArguments()} }}) {{
                            returning {{
                              id,
                              from_id
                              to_id
                            }}
                          }}
                        }}
                        ",
                OperationName = "UpdateLink",
            };
            var responseResult = _graphQlClient.SendMutationAsync<UpdateResponseType>(updateLinkRequest).AwaitResult();
                if (responseResult.Errors != null)
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            return handler?.Invoke(restriction, (Link<ulong>?)(responseResult.Data.update_links.returning[0])) ?? Constants.Continue;
        }

        public ulong Delete(IList<ulong>? restriction, WriteHandler<ulong>? handler)
        {
            var restrictionLink = new GqlLink(restriction, Constants.Any); 
            var updateLinkRequest = new GraphQLRequest { Query = $@"
                        mutation DeleteLink {{
                          delete_links(where: {{ {restrictionLink.ToGqlWhereArguments()} }}) {{
                            returning {{
                              id,
                              from_id
                              to_id
                            }}
                          }}
                        }}
                        ",
                OperationName = "DeleteLink" };
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

        public class GqlLink
    {
        public long? id;
        public long? from_id;
        public long? to_id;

        public GqlLink()
        {
            id = null;
            from_id = null;
            to_id = null;
        }

        public GqlLink(ulong anyConstant, params ulong[]? restriction) : this(restriction, anyConstant) {}

        [JsonConstructor]
        public GqlLink(IList<ulong>? link, ulong anyConstant)
        {

            if (link == null || link.Count == 0)
            {
                id = null;
                from_id = null;
                to_id = null;
            }
            else if (link.Count == 1)
            {
                if (!_equalityComparer.Equals(link[0], anyConstant))
                {
                    id = (long)link[0];
                }
                from_id = null;
                to_id = null;
            }
            else if (link.Count == 2)
            {
                id = null;
                if (!_equalityComparer.Equals(link[0], anyConstant))
                {
                    from_id = (long)link[0];
                }
                if (!_equalityComparer.Equals(link[1], anyConstant))
                {
                    to_id = (long)link[1];
                }
            }
            else if (link.Count == 3)
            {
                if (!_equalityComparer.Equals(link[0], anyConstant))
                {
                    id = (long)link[0];
                }
                if (!_equalityComparer.Equals(link[1], anyConstant))
                {
                    from_id = (long)link[1];
                }
                if (!_equalityComparer.Equals(link[2], anyConstant))
                {
                    to_id = (long)link[2];
                }
            }
            else
            {
                throw new ArgumentException("Invalid restriction");
            }
        }

        public string ToGqlParameters()
        {
            var sb = new StringBuilder();
            if (id != null)
            {
                sb.Append($"$id: bigint, ");
            }
            if (from_id != null)
            {
                sb.Append($"$from_id: bigint, ");
            }
            if (to_id != null)
            {
                sb.Append($"$to_id: bigint, ");
            }
            return sb.ToString();
        }

        public string ToGqlWhereArguments()
        {
            var sb = new StringBuilder();
            if (id != null)
            {
                sb.Append($"id: {{ _eq: {id} }}, ");
            }
            if (from_id != null)
            {
                sb.Append($"from_id: {{ _eq: {from_id} }}, ");
            }
            if (to_id != null)
            {
                sb.Append($"to_id: {{ _eq: {to_id} }}, ");
            }
            return sb.ToString();
        }

        public string ToGqlObjectArguments()
        {
            var sb = new StringBuilder();
            if (id != null)
            {
                sb.Append($"id: {id}, ");
            }
            if (from_id != null)
            {
                sb.Append($"from_id: {from_id}, ");
            }
            if (to_id != null)
            {
                sb.Append($"to_id: {to_id}, ");
            }
            return sb.ToString();
        }

        public static implicit operator Link<ulong>(GqlLink gqlLink) => new Link<ulong>((ulong)gqlLink.id.GetValueOrDefault(), (ulong)gqlLink.from_id.GetValueOrDefault(), (ulong)gqlLink.to_id.GetValueOrDefault());

        public static implicit operator GqlLink(Link<ulong> link) => new GqlLink(link.Index, link.Source, link.Target);
    }


    public class CreateResponseType
    {
        public GqlLink insert_links_one { get; set; }
    }

    public class UpdateResponseType
    {
        public UpdateLinks update_links { get; set; }

        public class UpdateLinks
        {
            public List<GqlLink> returning { get; set; }
        }
    }

    public class DeleteResponseType
    {
        public DeleteLinks delete_links { get; set; }

        public class DeleteLinks
        {
            public List<GqlLink> returning { get; set; }
        }
    }


    public class ReadLinksResponseType
    {
        public List<GqlLink> links { get; set; }
    }

    public class GetLinksResponseType
    {
        public List<GqlLink> links { get; set; }
    }
    }

}
