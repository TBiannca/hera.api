namespace Presentation.GraphQL.Base;

public class Schema : global::GraphQL.Types.Schema
{
    public Schema(IServiceProvider provider)
    {
        Mutation = new Mutation(provider);

        Query = new Query(provider);
    }
}