using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    public class LinksAggregateFields
    {
        public LinksAggregateFloatFields avg { get; set; }

        public int count { get; set; }

        public LinksAggregateBigIntFields max { get; set; }

        public LinksAggregateFloatFields min { get; set; }

        public LinksAggregateFloatFields stddev { get; set; }

        public LinksAggregateFloatFields stddev_pop { get; set; }

        public LinksAggregateFloatFields stddev_samp { get; set; }

        public LinksAggregateBigIntFields sum { get; set; }

        public LinksAggregateFloatFields var_pop { get; set; }

        public LinksAggregateFloatFields var_samp { get; set; }

        public LinksAggregateFloatFields variance { get; set; }

    }
}
