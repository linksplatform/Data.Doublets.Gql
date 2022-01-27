# -*- coding: utf-8 -*-
"""This module provides working with GraphQlClient.
"""
from typing import NoReturn, Dict, Any

from gql import Client, gql
from gql.transport.aiohttp import AIOHTTPTransport


class DeepClient:
    """Provides working with GraphQl server.
    """
    def __init__(self, address: str) -> NoReturn:
        """Initializes GraphQl client.
        
        :param address: graphql server address.
        """
        self.address = address
        self.client = Client(
            transport=AIOHTTPTransport(url=self.address),
            fetch_schema_from_transport=True
        )

    def query(self, q: str) -> Dict[str, Any]:
        """Sends the query to GraphQl server.

        :param q: GraphQL query.
        """
        return self.client.execute(gql(q))

