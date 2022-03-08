# -*- coding: utf-8 -*-
"""This module provides working with GraphQlClient."""
from typing import NoReturn, Dict, Any, Optional, List, Union

from gql import Client, gql
from gql.transport.aiohttp import AIOHTTPTransport

from python.deepclient.exceptions import DeepClientError


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

    @staticmethod
    def convert_to_query(value: Any) -> str:
        if isinstance(value, str):
            return 'string: {data: {value: "%s"}}' % (value,)
        elif isinstance(value, int | float):
            return 'number: {data: {value: %s}}' % (value,)
        elif isinstance(value, list | dict | bool):
            return 'object: {data: {value: %s}}' % (value,)
        elif value:
            raise DeepClientError('failure to convert %s type' % type(value))

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
        else:
            raise DeepClientError('_id param should be int or list of int, but got %s.' % type(_id))
        return self.query(
            '{ links %s { id type_id from_id to_id } }' % (where,))

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
        else:
            raise DeepClientError('_id param should be int or list of int, but got %s.' % type(_id))
        return self.query(
            '{ links %s { id type_id from_id to_id } }' % (where,))

    def insert_one(
            self,
            type_id: int,
            value: Optional[Any] = None,
            **kwargs: Any
    ) -> Dict[str, Any]:
        """Inserts one link into Links DB.

        :param type_id: unique type ID.
        :param value: object to inserting."""
        if not isinstance(type_id, int):
            raise DeepClientError('type_id should be int, but got %s' % type(type_id))
        kwargs['type_id'] = type_id
        obj = DeepClient.convert_to_query(value)
        if obj:
            kwargs['object'] = obj
        data = '''
            mutation {
              insert_links_one( object: { %s } )
              { id type_id from_id to_id}
            }''' % ','.join(['%s: %s' % (k, v) for k, v in kwargs.items()])
        return self.query(data)
