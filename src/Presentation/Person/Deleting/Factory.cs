using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using Presentation.Person.Types;

namespace Presentation.Person.Deleting;

public static class Factory
{
    public static FieldType Make(IServiceProvider provider)
    {
        return new FieldType()
        {
            Name = "deletePerson",
            Description = "Deletes a person.",
            Type = typeof(TPerson),
            Arguments = MakeArguments(),
            Resolver =  new FuncFieldResolver<TPerson, object>(MakeResolver(provider)),
        };
    }
    
    private static Func<IResolveFieldContext<TPerson>, object> MakeResolver(IServiceProvider provider)
    {
        return input => provider.GetService<IResolver>().Execute(input);
    }

    private static QueryArguments MakeArguments() => new (new QueryArgument(
        typeof(NonNullGraphType<IntGraphType>))
    {
        Name = "input",
        Description = "The id of the person to be deleted."
    });
}