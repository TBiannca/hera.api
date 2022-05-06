using global::GraphQL.Types;
using Presentation.Person.Creating;

namespace Presentation.GraphQL.Base;

public sealed class Mutation : ObjectGraphType
{
    public Mutation(IServiceProvider provider)
    {
        Name = "mutation";
        Description = "All the mutations that can be done.";

        AddField(Factory.Make(provider));
    }
}