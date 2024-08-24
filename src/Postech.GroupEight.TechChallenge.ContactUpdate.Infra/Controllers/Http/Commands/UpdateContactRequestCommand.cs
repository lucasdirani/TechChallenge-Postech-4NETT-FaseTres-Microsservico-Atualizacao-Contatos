using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands
{
    [ExcludeFromCodeCoverage]
    public record UpdateContactRequestCommand
    {
        public Guid ContactId { get; init; }
        public required UpdateContactNameRequestCommand ContactName { get; init; }
        public required UpdateContactEmailRequestCommand ContactEmail { get; init; }
        public required UpdateContactPhoneRequestCommand ContactPhone { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record UpdateContactNameRequestCommand
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record UpdateContactEmailRequestCommand
    {
        public required string Address { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record UpdateContactPhoneRequestCommand
    {
        public required string Number { get; init; }
        public required string AreaCode { get; init; }
    }
}