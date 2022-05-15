using Domain.Person.Models;

namespace Domain.Person.Creating.Commands;

public interface ICreatePerson : ICommand<IEnumerable<MCreatePerson>, IEnumerable<MPerson>>
{
}