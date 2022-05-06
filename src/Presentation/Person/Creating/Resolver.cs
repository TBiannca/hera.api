using Domain.Person.Models;
using GraphQL;

namespace Presentation.Person.Creating;

public class Resolver : IResolver
{
    public IEnumerable<MPerson> Execute(IResolveFieldContext<object> input)
    {
        throw new NotImplementedException();
    }
}