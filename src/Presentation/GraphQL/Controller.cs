using Microsoft.AspNetCore.Authorization;
using global::GraphQL;
using global::GraphQL.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc;
using Presentation.GraphQL.Base;

namespace Presentation.GraphQL
{
    [Route("graphql")]
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly Schema _schema;
        public Controller(Schema schema)
        {
            _schema = schema;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Post([FromBody] Dto query)
        {
            var result = await new DocumentExecuter().ExecuteAsync(config =>
            {
                config.Schema = _schema;
                config.Query = query.Query; 
                config.Inputs = query.Variables.ToInputs();
            });
            
            return await ToActionResult(result).ConfigureAwait(false);
        }
        
        private async Task<IActionResult> ToActionResult(ExecutionResult result)
        {
            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(await new DocumentWriter().WriteToStringAsync(result).ConfigureAwait(false));
        }
    }
}