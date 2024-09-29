using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Entities;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Outputs
{
    [ExcludeFromCodeCoverage]
    public record UpdateContactOutput
    {
        public Guid ContactId { get; init; }
        public bool IsContactNotifiedForUpdate { get; init; }
        public DateTime? ContactNotifiedForUpdateAt { get; init; }
        public required string ContactFirstName { get; init; }
        public required string ContactLastName { get; init; }
        public required string ContactEmail { get; init; }
        public required string ContactPhoneNumber { get; init; }
        public required string ContactPhoneNumberAreaCode { get; init; }

        internal static UpdateContactOutput CreateUsing(ContactEntity contactEntity, PublishedEventResult eventResult)
        {
            return new()
            {
                ContactId = contactEntity.Id,
                IsContactNotifiedForUpdate = eventResult.IsEventPublished(),
                ContactEmail = contactEntity.ContactEmail.Value,
                ContactFirstName = contactEntity.ContactName.FirstName,
                ContactLastName = contactEntity.ContactName.LastName,
                ContactPhoneNumber = contactEntity.ContactPhone.Number,
                ContactPhoneNumberAreaCode = contactEntity.ContactPhone.AreaCode.Value,
                ContactNotifiedForUpdateAt = eventResult.PublishedAt
            };
        }
    }
}