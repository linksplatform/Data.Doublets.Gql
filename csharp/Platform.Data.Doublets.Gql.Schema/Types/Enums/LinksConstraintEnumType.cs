using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    using MappedType = LinksConstraint;

    public class LinksConstraintEnumType : BaseEnumType<MappedType>
    {
        public LinksConstraintEnumType() : base("links_constraint", "unique or primary key constraints on table \"links\"")
        {
        }
    }
}
