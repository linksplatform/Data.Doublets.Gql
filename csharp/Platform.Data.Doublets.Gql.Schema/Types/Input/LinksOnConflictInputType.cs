﻿using Gql.Samples.Schemas.Link.Types;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    class LinksOnConflictInputType : InputObjectGraphType<LinksConstraint>
    {
        public LinksOnConflictInputType()
        {
            Field<LinksOnConflictInputType>("constraint");
            Field<ListGraphType<LinksSelectColumnEnumType>>("update_columns");
            Field<LinkBooleanExpressionInputType>("where");
        }
    }
}