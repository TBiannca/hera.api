using global::GraphQL.Types;
using GraphQL;
using GraphQL.Resolvers;
using Presentation.Person.Types;

namespace Presentation.Person.Fetching;

public class Factory
{
    public static FieldType Make(IServiceProvider provider)
    {
        return new ()
        {
            Name = "persons",
            Description = "Fetch all the existing persons.",
            Type = typeof(TPerson),
            Resolver = new FuncFieldResolver<TPerson, object>(MakeResolver(provider)),
        };
    }

    private static Func<IResolveFieldContext<object>, object> MakeResolver(IServiceProvider provider)
    {
        return input => provider.GetService<IResolver>().Execute(input);
    }
}