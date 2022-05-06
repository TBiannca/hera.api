using System.Diagnostics.CodeAnalysis;
using Domain.Person.Creating;
using GraphQL.Types;

namespace Presentation.Person.Creating.Types;

public sealed class TCreatePerson : InputObjectGraphType<MCreatePerson>
{
    public TCreatePerson()
    {
        Field(person => person.Name).Description("This is the Name");
        Field(person => person.Role).Description("This is the Role");
        Field(person => person.Descriptors).Description("Those are the Descriptors");
    }
}