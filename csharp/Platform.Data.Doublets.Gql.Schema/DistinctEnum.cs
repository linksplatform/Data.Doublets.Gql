using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    public enum distinct
    {
        from_id,
        id,
        to_id,
        type_id
    }
    class DistinctEnum : EnumerationGraphType<distinct>
    {
        public DistinctEnum()
        {
            Name = "DistinctEnum";
        }
    }
}
