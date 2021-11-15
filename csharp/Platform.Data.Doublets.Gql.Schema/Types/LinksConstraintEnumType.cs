using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    using MappedType = LinksConstraint;
    public class LinksConstraintEnumType : EnumerationGraphType<MappedType>
    {
        public LinksConstraintEnumType() => Name = "LinksConstraintEnum";
    }
}
