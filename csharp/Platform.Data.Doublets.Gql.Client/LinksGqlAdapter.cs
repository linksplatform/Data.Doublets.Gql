﻿using GraphQL;
using GraphQL.Client.Abstractions;
using Newtonsoft.Json.Linq;
using Platform.Collections;
using Platform.Data.Doublets.Gql.Schema;
using Platform.Data.Doublets.Gql.Schema.Types;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Platform.Data.Doublets.Gql.Client
{
    public class LinksGqlAdapter<TLink> : ILinks<TLink>
    {
        private IGraphQLClient _graphQlClient;

        public LinksGqlAdapter(IGraphQLClient graphQlClient, LinksConstants<TLink> linksConstants)
        {
            _graphQlClient = graphQlClient;
            Constants = linksConstants;
        }

        public TLink Count(IList<TLink> restriction)
        {
            throw new NotImplementedException();
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
            var response = _graphQlClient.SendMutationAsync<CreateResponseType>(createLinkRequest);
            response.Wait();
            var responseResult = response.Result;
            if (!responseResult.Errors.IsNullOrEmpty())
            {
                foreach (var responseResultError in responseResult.Errors!)
                {
                    throw new Exception(responseResultError.Message);
                }
            }
            return responseResult.Data.insert_links_one.id;
        }

        public TLink Update(IList<TLink> restrictions, IList<TLink> substitution) => throw new NotImplementedException();

        public void Delete(IList<TLink> restrictions) => throw new NotImplementedException();

        public LinksConstants<TLink> Constants { get; }
        public struct CreateResponseType
        {
            public InsertLinksOne insert_links_one { get; set; }
            public struct InsertLinksOne
            {
                public TLink id { get; set; }
            }
        }
    }
}
