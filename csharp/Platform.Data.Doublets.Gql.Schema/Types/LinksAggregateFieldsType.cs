using GraphQL;
using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    class LinksAggregateFieldsType : ObjectGraphType
    {
        public LinksAggregateFieldsType()
        {
            Field<LinksAggregateFloatFieldsInputType>("avg");
            Field<IntGraphType>("count", null, LinkQuery.Arguments, ResolveCount, null);
            Field<LinksAggregateBigIntFieldsType>("max");
            Field<LinksAggregateBigIntFieldsType>("min");
            Field<LinksAggregateFloatFieldsInputType>("stddev");
            Field<LinksAggregateFloatFieldsInputType>("stddev_pop");
            Field<LinksAggregateFloatFieldsInputType>("stddev_samp");
            Field<LongGraphType>("sum");
            Field<LinksAggregateFloatFieldsInputType>("var_pop");
            Field<LinksAggregateFloatFieldsInputType>("var_samp");
            Field<LinkInputType>("type");
            Field<LinksAggregateFloatFieldsInputType>("variance");
            Field<LongGraphType>("type_id");
        }

        private object ResolveCount(IResolveFieldContext<object> arg)
        {
            return 0;
        }
    }
}
