﻿using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    class LinksAggregateOrderByInputType : InputObjectGraphType
    {
        public LinksAggregateOrderByInputType()
        {
            Field<LinksFieldsOrderByInputType>("avg");
            Field<OrderByInputType>("count");
            Field<LinksFieldsOrderByInputType>("max");
            Field<LinksFieldsOrderByInputType>("min");
            Field<LinksFieldsOrderByInputType>("stddev");
            Field<LinksFieldsOrderByInputType>("stddev_pop");
            Field<LinksFieldsOrderByInputType>("stddev_samp");
            Field<LinksFieldsOrderByInputType>("sum");
            Field<LinksFieldsOrderByInputType>("var_pop");
            Field<LinksFieldsOrderByInputType>("var_samp");
            Field<LinksFieldsOrderByInputType>("type");
            Field<LinksFieldsOrderByInputType>("variance");
            Field<LinksFieldsOrderByInputType>("type_id");
        }
    }
}