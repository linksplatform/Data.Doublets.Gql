# -*- coding: utf-8 -*-
"""This module provides working with GraphQlClient."""
from typing import NoReturn, Dict, Any, Optional, List, Union
from json import dumps

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
            return 'object: {data: {value: %s}}' % (dumps(value),)
        elif value:
            raise DeepClientError('failure to convert %s type' % type(value))

    @staticmethod
    def convert_to_data(value: Any) -> str:
        if isinstance(value, str):
            return 'data: {value: "%s"}' % (value,)
        elif isinstance(value, int | float):
            return 'data: {value: %s}' % (value,)
        elif isinstance(value, list | dict | bool):
            return 'data: {value: %s}' % (dumps(value),)
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
            _id: Union[int, List[int]],
            *args: str,
    ) -> Dict[str, Any]:
        """Selects Link from Links DB

        :param _id: unique link ID.
        :param args: optional fields.
        """
        where = ''
        if isinstance(_id, int):
            where = '(where: {id: {_eq: %s}})' % (_id,)
        elif isinstance(_id, list):
            where = '(where: {id: {_in: %s}})' % (_id,)
        else:
            raise DeepClientError(
                '_id param should be int or list of int, but got %s.' % type(_id)
            )
        return self.query(
            '{ links %s { id from_id to_id type_id %s } }' % (where, ' '.join(args)))

    def select_by(
            self,
            key: str,
            value: Any,
            *args: str
    ) -> Dict[str, Any]:
        """Selects Link from Links DB by type_id, id, from_id, to_id, etc.

        :param key: what search
        :param value: value to search
        :param args: optional fields.
        """
        if isinstance(value, int | float):
            where = '(where: {%s: {_eq: %s}})' % (key, value)
        elif isinstance(value, str):
            where = '(where: {%s: {_eq: "%s"}})' % (key, value)
        elif isinstance(value, list):
            where = '(where: {%s: {_in: %s}})' % (key, value)
        else:
            raise DeepClientError(
                '_id param should be int or list of int, but got %s.' % type(value)
            )
        return self.query(
            '{ links %s { id type_id from_id to_id %s } }' % (where, ' '.join(args)))

    def select_with_options(
            self,
            options: str,
            *args: str
    ) -> Dict[str, Any]:
        """Selects links from Links DB with specified options"""
        return self.query(
            '{ links %s { %s } }' % (
                f'({options})' if options else '',
                ' '.join(args)
            )
        )

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
        obj = DeepClient.convert_to_data(value)
        if obj:
            kwargs['object'] = '{%s}' % obj
        data = '''
            mutation {
              insert_links_one( object: { %s } )
              { id type_id from_id to_id}
            }''' % ','.join(['%s: %s' % (k, v) for k, v in kwargs.items()])
        return self.query(data)

    def insert(
            self,
            *args: Dict[str, Any]
    ) -> Dict[str, Any]:
        """Inserts links into Links DB"""
        if len(args) == 0:
            return {}
        data = '''
            mutation {
              insert_links ( objects: [%s] ) {
                returning { id type_id from_id to_id }
              }
            }''' % ', '.join(
            '{ %s }' % ', '.join(['%s: %s' % (k, v) for k, v in obj.items()])
            for obj in args
        )
        return self.query(data)
