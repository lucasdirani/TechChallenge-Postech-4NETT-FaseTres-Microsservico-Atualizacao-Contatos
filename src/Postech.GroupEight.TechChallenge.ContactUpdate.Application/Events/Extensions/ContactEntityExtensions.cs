using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Entities;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Extensions
{
    [ExcludeFromCodeCoverage]
    internal static class ContactEntityExtensions
    {
        internal static ContactUpdatedEvent AsContactUpdatedEvent(this ContactEntity contactEntity)
        {
            return new()
            {
                ContactId = contactEntity.Id,
                ContactEmail = contactEntity.ContactEmail.Value,
                ContactFirstName = contactEntity.ContactName.FirstName,
                ContactLastName = contactEntity.ContactName.LastName,
                ContactPhoneNumber = contactEntity.ContactPhone.Number,
                ContactPhoneNumberAreaCode = contactEntity.ContactPhone.AreaCode.Value
            };
        }
    }
}