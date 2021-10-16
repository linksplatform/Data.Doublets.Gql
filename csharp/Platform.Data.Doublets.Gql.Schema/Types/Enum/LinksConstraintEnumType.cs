using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class LinksConstraintEnumType : BaseEnumType<LinksConstraint>
    {
        public LinksConstraintEnumType() : base("links_constraint", "unique or primary key constraints on table \"links\"")
        {

        }
    }
}
