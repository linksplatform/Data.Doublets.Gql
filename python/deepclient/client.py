# -*- coding: utf-8 -*-
"""This module provides working with GraphQlClient."""
from typing import NoReturn, Dict, Any, Optional, List, Union
from json import dumps
from re import sub

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
        """Converts value to gql query"""
        if isinstance(value, str):
            return 'string: {data: {value: "%s"}}' % (value,)
        elif isinstance(value, int | float):
            return 'number: {data: {value: %s}}' % (value,)
        elif isinstance(value, list | dict | bool):
            return 'object: {data: {value: %s}}' % (DeepClient.dump_json(value),)
        elif value:
            raise DeepClientError('failure to convert %s type' % type(value))

    @staticmethod
    def convert_to_data(value: Any) -> str:
        """Converts value to gql query data"""
        if isinstance(value, str):
            return 'data: {value: "%s"}' % (value,)
        elif isinstance(value, int | float | bool):
            return 'data: {value: %s}' % (value,)
        elif isinstance(value, list | dict):
            return 'data: {value: %s}' % (DeepClient.dump_json(value),)
        elif value:
            raise DeepClientError('failure to convert %s type' % type(value))

    @staticmethod
    def dump_json(value: Dict[str, Any]) -> str:
        """Dumps json to GQL-like representation"""
        if isinstance(value, dict):
            return sub(r'"([\S ]+?)"\s*:', r'\1:', dumps(value))
        raise DeepClientError('failure to convert %s type' % type(value))

    def delete(
            self,
            where: Dict[str, Any],
            *args: str,
            table: str = 'links'
    ) -> Dict[str, Any]:
        """Deletes matched links

        :param where: match options
        :param table: table type"""
        data = f'''
            mutation {{
                delete_{table} (where: {DeepClient.dump_json(where)}) {{
                returning {{ {' '.join(args)} }} }} }}
        '''
        return self.query(data)

    def query(
            self,
            q: str
    ) -> Dict[str, Any]:
        """Sends the query to GraphQl server.

        :param q: GraphQL query.
        """
        return self.client.execute(gql(q))

    def insert_one(
            self,
            type_id: int,
            value: Optional[Any] = None,
            *args: str,
            table: str = 'links',
            **kwargs: Any
    ) -> Dict[str, Any]:
        """Inserts one link into Links DB.

        :param type_id: unique type ID.
        :param value: object to inserting.
        :param table: table type"""
        if not isinstance(type_id, int):
            raise DeepClientError('type_id should be int, but got %s' % type(type_id))
        kwargs['type_id'] = type_id
        obj = DeepClient.convert_to_data(value)
        if obj:
            kwargs['object'] = '{%s}' % obj
        data = f'''
            mutation {{
              insert_{table}_one(
                object: {{
                    {','.join(['%s: %s' % (k, v) for k, v in kwargs.items()])}
                }})
              {{ {' '.join(args)} }}
            }}'''
        return self.query(data)

    def insert(
            self,
            *args: Dict[str, Any],
            table: str = 'links'
    ) -> Dict[str, Any]:
        """Inserts links into Links DB

        :param table: table type"""
        if len(args) == 0:
            return {}
        data = f'''
            mutation {{
              insert_{table} ( objects: [{', '.join(DeepClient.dump_json(obj) for obj in args)}] ) {{
                returning {{ id type_id from_id to_id object {{ value }} }}
              }}
            }}'''
        return self.query(data)

    def select(
            self,
            _id: Union[int, List[int]],
            *args: str,
            table: str = 'links'
    ) -> Dict[str, Any]:
        """Selects Link from Links DB

        :param _id: unique link ID.
        :param args: optional fields.
        :param table: table type.
        """
        if isinstance(_id, int):
            where = '(where: {id: {_eq: %s}})' % (_id,)
        elif isinstance(_id, list):
            where = '(where: {id: {_in: %s}})' % (_id,)
        else:
            raise DeepClientError(
                '_id param should be int or list of int, but got %s.' % type(_id)
            )
        return self.query(
            '{ %s %s { id from_id to_id type_id %s } }' % (table, where, ' '.join(args)))

    def select_by(
            self,
            key: str,
            value: Any,
            *args: str,
            table: str = 'links'
    ) -> Dict[str, Any]:
        """Selects Link from Links DB by type_id, id, from_id, to_id, etc.

        :param key: what search
        :param value: value to search
        :param args: optional fields.
        :param table: table type
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
            '{ %s %s { id type_id from_id to_id %s } }' % (table, where, ' '.join(args)))

    def select_with_options(
            self,
            options: str,
            *args: str,
            table: str = 'links'
    ) -> Dict[str, Any]:
        """Selects links from Links DB with specified options

        :param table: table type
        :param options: select options"""
        return self.query(
            '{ %s %s { %s } }' % (
                table,
                f'({options})' if options else '',
                ' '.join(args)
            )
        )

    def update(
            self,
            where: Dict[str, Any],
            _set: Dict[str, Any],
            *args: str,
            table: str = 'links'
    ) -> Dict[str, Any]:
        """Updates all matched links

        :param _set: new link data
        :param where: match options
        :param table: table type"""
        if len(args) == 0:
            return {}
        data = '''
            mutation { update_%s(_set: %s, where: %s){
                returning { %s }
            }}
        ''' % (
            table,
            DeepClient.dump_json(_set), DeepClient.dump_json(where),
            ' '.join(args)
        )
        return self.query(data)
