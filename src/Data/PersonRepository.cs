using Domain.Person.Creating;
using Domain.Person.Models;
using Domain.Person.Repositories;

namespace Data;

public class PersonRepository : IPersonRepository
{
    private readonly Context _context;

    public PersonRepository(Context context) => _context = context;
    
    public IEnumerable<MPerson> Insert(IEnumerable<MCreatePerson> input) => input
        .Select(MakeEntity)
        .Select(InsertPerson)
        .Select(MakeModel)
        .ToList();

    public MPerson Delete(int input) => _context.Persons
        .Where(person => person.Id == input).ToList()
        .Select(DeletePerson)
        .Select(MakeModel)
        .First();

    public IEnumerable<MPerson> GetAll() => _context.Persons
        .Select(MakeModel)
        .ToList();

    private EPerson InsertPerson(EPerson entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
        return entity;
    }
    
    private EPerson DeletePerson(EPerson entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
        return entity;
    }
    
    private static EPerson MakeEntity(MCreatePerson model) => new EPerson
    {
        Name = model.Name,
        Role = model.Role,
        Descriptors = model.Descriptors,
    };

    private static MPerson MakeModel(EPerson entity) => new MPerson
    {
        Name = entity.Name,
        Role = entity.Role,
        Descriptors = entity.Descriptors,
    };
}
