using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Entities;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.ValueObjects;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Inputs
{
    [ExcludeFromCodeCoverage]
    public record UpdateContactInput
    {
        public Guid ContactId { get; init; }
        public required CurrentContactDataInput CurrentContactData { get; init; }
        public required UpdatedContactDataInput UpdatedContactData { get; init; }

        internal ContactEntity AsContactEntity()
        {
            ContactNameValueObject contactName = new(CurrentContactData.ContactFirstName, CurrentContactData.ContactLastName);
            ContactEmailValueObject contactEmail = new(CurrentContactData.ContactEmail);
            ContactPhoneValueObject contactPhone = new(CurrentContactData.ContactPhoneNumber, AreaCodeValueObject.Create(CurrentContactData.ContactPhoneNumberAreaCode));
            return new(ContactId, contactName, contactEmail, contactPhone);
        }
    }

    [ExcludeFromCodeCoverage]
    public record CurrentContactDataInput
    {
        public required string ContactFirstName { get; init; }
        public required string ContactLastName { get; init; }
        public required string ContactEmail { get; init; }
        public required string ContactPhoneNumber { get; init; }
        public required string ContactPhoneNumberAreaCode { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record UpdatedContactDataInput
    {
        public required string ContactFirstName { get; init; }
        public required string ContactLastName { get; init; }
        public required string ContactEmail { get; init; }
        public required string ContactPhoneNumber { get; init; }
        public required string ContactPhoneNumberAreaCode { get; init; }

        internal ContactPhoneValueObject AsContactPhoneValueObject()
        {
            return new(ContactPhoneNumber, AreaCodeValueObject.Create(ContactPhoneNumberAreaCode));
        }
    }
}