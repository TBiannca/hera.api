using Domain;
using Domain.Person.Models;
using GraphQL;

namespace Presentation.Person.Deleting;

public interface IResolver : ICommand<IResolveFieldContext<object>, MPerson>
{
}