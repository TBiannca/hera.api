using global::GraphQL.Types;

namespace Presentation.GraphQL.Base;

public sealed class Mutation : ObjectGraphType
{
    public Mutation(IServiceProvider provider)
    {
        Name = "mutation";
        Description = "All the mutations that can be done.";

        AddField(Person.Creating.Factory.Make(provider));
        AddField(Person.Deleting.Factory.Make(provider));
    }
}