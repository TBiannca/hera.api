using Domain;
using Domain.Person.Models;
using GraphQL;

namespace Presentation.Person.Creating;

public interface IResolver : ICommand<IResolveFieldContext<object>, IEnumerable<MPerson>>
{
}