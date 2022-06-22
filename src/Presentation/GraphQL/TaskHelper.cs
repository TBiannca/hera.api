using GraphQL.Validation;

namespace Presentation.GraphQL;

internal static class TaskHelper
{
    public static Task<INodeVisitor> ToTask(this INodeVisitor visitor) => Task.FromResult(visitor);
}