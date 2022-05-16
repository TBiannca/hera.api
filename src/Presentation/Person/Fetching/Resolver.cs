using Data;
using Domain.Person.Models;
using Domain.Person.Repositories;

namespace Presentation.Person.Fetching;

public class Resolver : IResolver
{
    private readonly IPersonRepository _person;

    public Resolver(IPersonRepository person) => _person = person;

        public IEnumerable<MPerson> Execute()
    {
        return _person.GetAll();
    }
}