using Domain.Person.Models;
using Domain.Person.Repositories;
using GraphQL;

namespace Presentation.Person.Deleting;

public class Resolver : IResolver
{
    private readonly IPersonRepository _person;

    public Resolver(IPersonRepository person) => _person = person;

    public MPerson Execute(IResolveFieldContext<object> input)
    {
        var personId = input.GetArgument<int>("input");
        return _person.Delete(personId);
    }
}