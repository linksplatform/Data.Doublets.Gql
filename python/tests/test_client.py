# -*- coding: utf-8 -*-
"""Testing around GraphQlClient."""
from unittest import TestCase, main

from graphql import GraphQLError

from __init__ import DeepClient, DeepClientError
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
        print(self.client.select_by("type_id", 1))

    def test_4_deep_client_error(self):
        try:
            self.client.select("invalid")
        except DeepClientError as e:
            print(e)
        try:
            self.client.select_by("type_id", "invalid")
        except DeepClientError as e:
            print(e)
        try:
            self.client.insert_one('l', 50)
        except DeepClientError as e:
            print(e)

    def test_5_create_new_type(self):
        """Creates a new type if available"""
        response = self.client.select_with_options(
            '''where: {
                type_id: {_eq: 1}
                object: {value: {_eq: "DeepClientTestType"}}
            }''',  # options
            'id', 'type_id', 'object {value}'  # args
        )
        print(response)
        if len(response['links']) == 0:
            response = self.client.insert_one(1, "DeepClientTestType")
            print(response)
        else:
            response = self.client.insert_one(
                response['links'][0]['id'],
                {"text": "Hello, world!"},
                'id', 'object { value }', 'type_id'
            )
            print(response)

    def test_6_insert(self):
        response = ''


if __name__ == '__main__':
    main()
