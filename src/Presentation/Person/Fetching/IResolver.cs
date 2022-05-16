using Data;
using Domain;
using Domain.Person.Models;

namespace Presentation.Person.Fetching;

public interface IResolver : IOutputCommand<IEnumerable<MPerson>>
{
}