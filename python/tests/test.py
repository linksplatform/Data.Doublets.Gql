# -*- coding: utf-8 -*-
"""Testing around GraphQlClient.
"""
from unittest import TestCase, main

from graphql import GraphQLError

from __init__ import DeepClient


class GraphQlClientTest(TestCase):
    """Provides GraphQl client testing.
    """
    client = DeepClient("http://localhost:60341/v1/graphql")

    def test_query(self):
        """Test shows what happens on valid query.
        """
        response = self.client.query('''
            {
              links {
                id
                from_id
                to_id
              }
            }''')
        print(response)

    def test_failure_query(self):
        """Test shows what happens on valid query.
        """
        try:
            response = self.client.query('''
                {
                  links {
                    error
                    from_id
                    to_id
                  }
                }''')
            print(response)
        except GraphQLError as e:
            print('error: ', e)


if __name__ == '__main__':
    main()
