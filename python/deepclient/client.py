# -*- coding: utf-8 -*-
"""This module provides working with GraphQlClient.
"""
from typing import NoReturn, Dict, Any, Optional, overload

from gql import Client, gql
from gql.transport.aiohttp import AIOHTTPTransport


class DeepClient:
    """Provides working with GraphQl server."""
    def __init__(
            self,
            address: str,
            headers: Optional[Dict[str, Any]] = None
    ) -> NoReturn:
        """Initializes GraphQl client.
        
        :param address: graphql server address.
        """
        self.address = address
        self.client = Client(
            transport=AIOHTTPTransport(url=self.address, headers=headers)
        )

    def query(
            self,
            q: str
    ) -> Dict[str, Any]:
        """Sends the query to GraphQl server.

        :param q: GraphQL query.
        """
        return self.client.execute(gql(q))

    def select(
            self,
            _id: int
    ) -> Dict[str, Any]:
        """Selects Link from Links DB"""
        return self.query('{ links { id } }')

    @overload
    def insert(
            self,
            from_id: int,
            to_id: int,
            value: Any
    ) -> Dict[str, Any]:
        pass

