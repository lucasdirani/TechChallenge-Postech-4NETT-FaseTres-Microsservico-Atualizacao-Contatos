using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Inputs;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands
{
    [ExcludeFromCodeCoverage]
    public record UpdateContactRequestCommand
    {
        [JsonPropertyName("contactId")]
        public Guid ContactId { get; init; }

        [JsonPropertyName("currentContactData")]
        public required CurrentContactDataRequestCommand CurrentContactData { get; init; }

        [JsonPropertyName("updatedContactData")]
        public required UpdatedContactDataRequestCommand UpdatedContactData { get; init; }

        internal UpdateContactInput AsUpdateContactInput()
        {
            return new()
            {
                ContactId = ContactId,
                CurrentContactData = new()
                {
                    ContactFirstName = CurrentContactData.ContactName.FirstName,
                    ContactLastName = CurrentContactData.ContactName.LastName,
                    ContactEmail = CurrentContactData.ContactEmail.Address,
                    ContactPhoneNumber = CurrentContactData.ContactPhone.Number,
                    ContactPhoneNumberAreaCode = CurrentContactData.ContactPhone.AreaCode
                },
                UpdatedContactData = new()
                {
                    ContactFirstName = UpdatedContactData.ContactName.FirstName,
                    ContactLastName = UpdatedContactData.ContactName.LastName,
                    ContactEmail = UpdatedContactData.ContactEmail.Address,
                    ContactPhoneNumber = UpdatedContactData.ContactPhone.Number,
                    ContactPhoneNumberAreaCode = UpdatedContactData.ContactPhone.AreaCode
                },
            };
        }

        internal bool HasSameContactId(Guid contactId)
        {
            return ContactId.Equals(contactId);
        }
    }

    [ExcludeFromCodeCoverage]
    public record CurrentContactDataRequestCommand
    {
        [JsonPropertyName("contactName")]
        public required ContactNameRequestCommand ContactName { get; init; }

        [JsonPropertyName("contactEmail")]
        public required ContactEmailRequestCommand ContactEmail { get; init; }

        [JsonPropertyName("contactPhone")]
        public required ContactPhoneRequestCommand ContactPhone { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record UpdatedContactDataRequestCommand
    {
        [JsonPropertyName("contactName")]
        public required ContactNameRequestCommand ContactName { get; init; }

        [JsonPropertyName("contactEmail")]
        public required ContactEmailRequestCommand ContactEmail { get; init; }

        [JsonPropertyName("contactPhone")]
        public required ContactPhoneRequestCommand ContactPhone { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record ContactNameRequestCommand
    {
        [JsonPropertyName("firstName")]
        public required string FirstName { get; init; }

        [JsonPropertyName("lastName")]
        public required string LastName { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record ContactEmailRequestCommand
    {
        [JsonPropertyName("address")]
        public required string Address { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record ContactPhoneRequestCommand
    {
        [JsonPropertyName("number")]
        public required string Number { get; init; }

        [JsonPropertyName("areaCode")]
        public required string AreaCode { get; init; }
    }
}