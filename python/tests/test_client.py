# -*- coding: utf-8 -*-
"""Testing around GraphQlClient."""
from unittest import TestCase, main

from graphql import GraphQLError

from __init__ import DeepClient
from config import GQL_URL, GQL_TOKEN


class GraphQlClientTest(TestCase):
    """Provides GraphQl client testing."""
    client = DeepClient(
        GQL_URL, {
            "Authorization": GQL_TOKEN
        }
    )

    def test_query(self):
        """Test shows what happens on valid query."""
        response = self.client.query('''
            query MyQuery {
              links {
                id
              }
            }''')
        print(response)

    def test_query_failure(self):
        """Test shows what happens on valid query."""
        try:
            self.client.query('''
                query MyQuery {
                  links {
                    not_exist_value
                  }
                }''')
        except GraphQLError as e:
            print('error: ', e)


if __name__ == '__main__':
    main()
