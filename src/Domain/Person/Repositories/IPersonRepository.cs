using Domain.Person.Creating;
using Domain.Person.Models;

namespace Domain.Person.Repositories;

public interface IPersonRepository
{
    public IEnumerable<MPerson> Insert(IEnumerable<MCreatePerson> input);
    public MPerson Delete(int input);

    public IEnumerable<MPerson> GetAll();
}