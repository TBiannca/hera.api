using global::GraphQL.Types;
using Presentation.Person.Fetching;

namespace Presentation.GraphQL.Base;

public sealed class Query : ObjectGraphType
{
    public Query(IServiceProvider provider)
    {
        Name = "query";
        Description = "All the queries that can be done.";

        AddField(Factory.Make(provider));
    }
}