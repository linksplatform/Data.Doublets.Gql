using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types
{

    internal class LinksConstraintEnumType : EnumerationGraphType<LinksConstraint>
    {
        public LinksConstraintEnumType()
        {
            Name = "LinksConstraintEnum";
        }
    }
}
