using Newtonsoft.Json.Linq;

namespace Presentation.GraphQL.Base;

public class Dto
{
    public string Query { get; set; }

    public JObject Variables { get; set; }
}