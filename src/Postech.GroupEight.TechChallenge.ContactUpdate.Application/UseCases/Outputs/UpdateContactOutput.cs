using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Entities;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.ValueObjects;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Outputs
{
    [ExcludeFromCodeCoverage]
    public record UpdateContactOutput
    {
        public Guid ContactId { get; init; }
        public DateTime ContactNotifiedForUpdateAt { get; init; }
        public required string ContactFirstName { get; init; }
        public required string ContactLastName { get; init; }
        public required string ContactEmail { get; init; }
        public required string ContactPhoneNumber { get; init; }

        internal static UpdateContactOutput CreateUsing(ContactEntity contactEntity, PublishedEventResult eventResult)
        {
            return new()
            {
                ContactId = contactEntity.Id,
                ContactEmail = contactEntity.ContactEmail.Value,
                ContactFirstName = contactEntity.ContactName.FirstName,
                ContactLastName = contactEntity.ContactName.LastName,
                ContactPhoneNumber = ContactPhoneValueObject.Format(contactEntity.ContactPhone.AreaCode.Value, contactEntity.ContactPhone.Number),
                ContactNotifiedForUpdateAt = eventResult.PublishedAt
            };
        }
    }
}