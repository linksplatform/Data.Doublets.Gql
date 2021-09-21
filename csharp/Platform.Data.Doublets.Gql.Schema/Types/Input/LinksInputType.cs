using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class LinksInputType : InputObjectGraphType<Links>
    {
        public LinksInputType()
        {
            Field(x => x.id, true, typeof(LongGraphType));
            Field(x => x.from_id, true, typeof(LongGraphType));
            Field(x => x.to_id, true, typeof(LongGraphType));
            Field(x => x.type_id, true, typeof(LongGraphType));
        }
    }
}
