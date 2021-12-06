# -*- coding: utf-8 -*-
"""Testing around GraphQlClient.
"""
import unittest
from sys import path

path.append('../')
from linksgql import GraphQlClient, GraphQlQueryError


class GraphQlClientTest(unittest.TestCase):
    """Provides GraphQl client testing.
    """
    client = GraphQlClient("http://localhost:60341/v1/graphql")

    def test_query(self):
        """Test shows what happens on valide query.
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

    def test_query_error_handling(self):
        """Test shows what happens on invalide query.
        """
        try:
            response = self.client.query('''
                {
                  links {
                    a
                  }
                }''')
        except GraphQlQueryError as e:
            print('GraphQlQueryError: ', e)


if __name__ == '__main__':
    unittest.main()
