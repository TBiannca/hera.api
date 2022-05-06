using Domain.Person.Models;
using global::GraphQL.Types;

namespace Presentation.Person.Types;

public class TPerson : ObjectGraphType<MPerson>
{
    public TPerson()
    {
        Field(person => person.Id).Description("This is the person id.");
        Field(person => person.Name).Description("This is the person's name.");
        Field(person => person.Role).Description("This is the person's role.");
        Field(person => person.Descriptors).Description("Those are the descriptors calculated by detection model.");
    }
}