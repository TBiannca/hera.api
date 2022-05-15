using Domain.Person.Models;
using Domain.Person.Repositories;

namespace Domain.Person.Creating.Commands;

public class CreatePerson : ICreatePerson
{
    private readonly IPersonRepository _repository;

    public CreatePerson(IPersonRepository repository) => _repository = repository;

    public IEnumerable<MPerson> Execute(IEnumerable<MCreatePerson> input) => _repository.Insert(input);
}