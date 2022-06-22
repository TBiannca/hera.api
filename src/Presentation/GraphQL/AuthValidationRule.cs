using System.Security.Claims;
using GraphQL.Language.AST;
using GraphQL.Validation;

namespace Presentation.GraphQL;

public class AuthValidationRule : IValidationRule
{
    private static readonly Task<INodeVisitor> _nodeVisitor = new MatchingNodeVisitor<Operation>((op, context) =>
    {
        var userContext = (context.UserContext["ident"]) as ClaimsIdentity;
        if (!userContext.IsAuthenticated)
        {
               
            if (((Field)op.SelectionSet.Selections[0]).Name != "login")
            {
                context.ReportError(new ValidationError(context.Document.OriginalQuery + "___", "auth-required", "login"));
            }
        }
    }).ToTask();

    public Task<INodeVisitor> ValidateAsync(ValidationContext context) => _nodeVisitor;
}
