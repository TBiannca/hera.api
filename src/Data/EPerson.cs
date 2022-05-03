using System.Text.Json.Nodes;

namespace Data;

public class EPerson
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Descriptors { get; set; }
}