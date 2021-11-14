using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksOnConflict;

    public class LinksOnConflictInputType : InputObjectGraphType<MappedType>
    {
        public LinksOnConflictInputType()
        {
            Name = "links_on_conflict";
            Field<NonNullGraphType<LinksConstraintEnumType>>(nameof(MappedType.constraint));
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<LinksUpdateColumnEnumBaseType>>>>(
                nameof(MappedType.update_columns));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.where));
        }
    }
}
