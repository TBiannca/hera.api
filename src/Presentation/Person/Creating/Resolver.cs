using Domain.Person.Creating;
using Domain.Person.Creating.Commands;
using Domain.Person.Models;
using GraphQL;

namespace Presentation.Person.Creating;

public class Resolver : IResolver
{
    private readonly ICreatePerson _createPerson;
    public Resolver(ICreatePerson createPerson) => _createPerson = createPerson;
    public IEnumerable<MPerson> Execute(IResolveFieldContext<object> input)
    {
        var person = input.GetArgument<IEnumerable<MCreatePerson>>("input");
        return _createPerson.Execute(person);
    }
}