# -*- coding: utf-8 -*-
"""This module provides working with GraphQlClient.
"""
from typing import NoReturn

from requests import Session
from .exceptions import GraphQlQueryError


class GraphQlClient:
    """Provides working with GraphQl server.
    """
    def __init__(self, address: str) -> NoReturn:
        """Initializes GraphQl client.
        
        :param address: graphql server address.
        """
        self.address = address
        self.session = Session()

    def query(self, q: str) -> dict:
        """Sends the query to GraphQl server.

        :param q: GraphQL query.
        """
        response = self.session.post(self.address, json={'query': q}).json()
        if "errors" in response:
            raise GraphQlQueryError(response["errors"])
        return response
