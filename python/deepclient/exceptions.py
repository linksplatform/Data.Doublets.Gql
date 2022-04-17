# -*- coding: utf-8 -*-
"""This module describes all kinds of errors received."""


class DeepClientError(Exception):
    """Raises when error in method call"""


class GraphQlQueryError(Exception):
    """Raises when sends wrong GraphQL query."""
