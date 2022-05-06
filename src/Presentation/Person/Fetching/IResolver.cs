using Domain;
using Domain.Person.Models;
using GraphQL;

namespace Presentation.Person.Fetching;

public interface IResolver : ICommand<IResolveFieldContext<object>, IEnumerable<MPerson>>
{
}