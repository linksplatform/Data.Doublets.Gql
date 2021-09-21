using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class LinksFieldsOrderByInputType : InputObjectGraphType<LinksFieldsOrderBy>
    {
        public LinksFieldsOrderByInputType()
        {
        }

        public LinksFieldsOrderByInputType(string name)
        {
            Name = name;
        }
    }
}