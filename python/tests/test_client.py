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

    def test_1_query(self):
        """Test shows what happens on valid query."""
        response = self.client.query('''
            query MyQuery {
              links {
                id
              }
            }''')
        print(response)

    def test_2_query_failure(self):
        """Test shows what happens on invalid query."""
        try:
            self.client.query('''
                query MyQuery {
                  links {
                    not_exist_value
                  }
                }''')
        except GraphQLError as e:
            print('error: ', e)

    def test_3_select(self):
        print(self.client.select(75))
        print(self.client.select([1, 2]))

    def test_4_select(self):
        print(self.client.select_by("type_id", 1))

    def test_5_deep_client_error(self):
        self.client.select("invalid")
        self.client.select_by("type_id", "invalid")
        self.client.insert_one('l', 50)

    def test_6_create_new_type(self):
        """Creates a new type if available"""
        response = self.client.query('''
            {
                links(where: {
                    type_id: {_eq: 1}
                    string: {value: {_eq: "DeepClientTestType"}}
                })
                {id type_id}
            }''')
        print(response)
        if len(response['links']) == 0:
            response = self.client.insert_one(1, "DeepClientTestType")
            print(response)
        else:
            response = self.client.insert_one(
                response['links'][0]['id'],
                {"text": "Hello, world!"},
                to_id=100, from_id=10)
            print(response)


if __name__ == '__main__':
    main()
