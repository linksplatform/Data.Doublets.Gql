using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    public class LinksConstraintEnumType : BaseEnumType<LinksConstraint>
    {
        public LinksConstraintEnumType() : base("links_constraint",
            "unique or primary key constraints on table \"links\"")
        {
        }
    }
}