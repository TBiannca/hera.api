using global::GraphQL.Types;
using GraphQL;
using GraphQL.Resolvers;
using Presentation.Person.Creating.Types;
using Presentation.Person.Types;

namespace Presentation.Person.Creating;

public static class Factory
{
    public static FieldType Make(IServiceProvider provider)
    {
        return new FieldType()
        {
            Name = "createPerson",
            Description = "Creates a person.",
            Type = typeof(NonNullGraphType<ListGraphType<NonNullGraphType<TPerson>>>),
            Arguments = MakeArguments(),
            Resolver =  new FuncFieldResolver<TCreatePerson, object>(MakeResolver(provider)),
        };
    }
    
    private static Func<IResolveFieldContext<TCreatePerson>, object> MakeResolver(IServiceProvider provider)
    {
        return input => provider.GetService<IResolver>().Execute(input);
    }

    private static QueryArguments MakeArguments() => new (new QueryArgument(
        typeof(NonNullGraphType<ListGraphType<NonNullGraphType<TCreatePerson>>>))
    {
        Name = "input",
        Description = "The person to be created."
    });
}