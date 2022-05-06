namespace Presentation.GraphQL
{
    using System.Threading.Tasks;
    using global::GraphQL;
    using global::GraphQL.NewtonsoftJson;
    using Microsoft.AspNetCore.Mvc;
    using Presentation.GraphQL.Base;

    [Route("graphql")]
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly Schema _schema;

        public Controller(Schema schema) => _schema = schema;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dto query)
        {
            var result = await Execute(query).ConfigureAwait(false);
            return HandleResult(result);
        }

        private async Task<ExecutionResult> Execute(Dto query) => await new DocumentExecuter().ExecuteAsync(configure =>
        {
            configure.Schema = _schema;
            configure.Query = query.Query;
            configure.Variables = query.Variables.ToInputs();
        }).ConfigureAwait(false);

        private IActionResult HandleResult(ExecutionResult result) => result.Errors != null
            ? BadRequest(new { ValidationErrors = result.Errors.Select(error => error.ToString()) })
            : Ok(new { result.Data });
    }
}