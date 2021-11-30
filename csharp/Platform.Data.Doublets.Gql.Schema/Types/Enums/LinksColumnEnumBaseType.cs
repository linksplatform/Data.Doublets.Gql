namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    using MappedType = LinksColumn;

    public class LinksColumnEnumBaseType : BaseEnumType<MappedType>
    {
        public LinksColumnEnumBaseType() : base(default)
        {
        }

        public LinksColumnEnumBaseType(string name) : base(name)
        {
        }

        public LinksColumnEnumBaseType(string name, string description) : base(name, description)
        {
        }
    }
}
