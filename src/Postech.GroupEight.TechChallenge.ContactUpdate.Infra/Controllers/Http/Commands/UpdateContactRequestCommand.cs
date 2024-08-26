using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands
{
    [ExcludeFromCodeCoverage]
    public record UpdateContactRequestCommand
    {
        [JsonPropertyName("contactId")]
        public Guid ContactId { get; init; }

        [JsonPropertyName("contactName")]
        public required UpdateContactNameRequestCommand ContactName { get; init; }

        [JsonPropertyName("contactEmail")]
        public required UpdateContactEmailRequestCommand ContactEmail { get; init; }

        [JsonPropertyName("contactPhone")]
        public required UpdateContactPhoneRequestCommand ContactPhone { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record UpdateContactNameRequestCommand
    {
        [JsonPropertyName("firstName")]
        public required string FirstName { get; init; }

        [JsonPropertyName("lastName")]
        public required string LastName { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record UpdateContactEmailRequestCommand
    {
        [JsonPropertyName("address")]
        public required string Address { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record UpdateContactPhoneRequestCommand
    {
        [JsonPropertyName("number")]
        public required string Number { get; init; }

        [JsonPropertyName("areaCode")]
        public required string AreaCode { get; init; }
    }
}