# -*- coding: utf-8 -*-
"""This module provides working with GraphQlClient.
"""
from typing import NoReturn, Dict, Any, Optional, overload, List, Union

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
            _id: Union[int, List[int]]
    ) -> Dict[str, Any]:
        """Selects Link from Links DB"""
        where = ''
        if isinstance(_id, int):
            where = '(where: {id: {_eq: %s}})' % (_id,)
        elif isinstance(_id, list):
            where = '(where: {id: {_in: %s}})' % (_id,)
        return self.query('''
            {
                links %s
                { id type_id from_id to_id }
            }''' % (where,))

    def select_by_type_id(
            self,
            _id: Union[int, List[int]]
    ) -> Dict[str, Any]:
        """Selects Link from Links DB by type_id

        :param _id: type_id
        """
        where = ''
        if isinstance(_id, int):
            where = '(where: {type_id: {_eq: %s}})' % (_id,)
        elif isinstance(_id, list):
            where = '(where: {type_id: {_in: %s}})' % (_id,)
        return self.query('''
            {
                links %s
                { id type_id from_id to_id }
            }''' % (_id,))

    @overload
    def insert(
            self,
            from_id: int,
            to_id: int,
            value: Any
    ) -> Dict[str, Any]:
        pass

