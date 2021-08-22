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

    internal class LinksSelectColumnEnum : EnumerationGraphType<distinct>
    {
        public LinksSelectColumnEnum() => Name = "LinksSelectColumnEnum";
    }
}
