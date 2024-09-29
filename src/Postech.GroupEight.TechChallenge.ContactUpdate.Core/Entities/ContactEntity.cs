using Postech.GroupEight.TechChallenge.ContactUpdate.Core.ValueObjects;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Core.Entities
{
    public class ContactEntity : EntityBase
    {
        public ContactEntity(
            Guid contactId,
            ContactNameValueObject contactName,
            ContactEmailValueObject contactEmail,
            ContactPhoneValueObject contactPhone)
            : base(contactId)
        {
            ContactName = contactName;
            ContactEmail = contactEmail;
            ContactPhone = contactPhone;
        }

        private ContactEntity() { }

        public ContactNameValueObject ContactName { get; private set; }

        public ContactEmailValueObject ContactEmail { get; private set; } 

        public ContactPhoneValueObject ContactPhone { get; private set; }

        /// <summary>
        /// Updates the contact's name if their first name or last name has changed.
        /// </summary>
        /// <param name="contactFirstName">The contact's new first name.</param>
        /// <param name="contactLastName">The contact's new last name.</param>
        public void UpdateContactName(string contactFirstName, string contactLastName)
        {
            if (ContactName.HasBeenChanged(contactFirstName, contactLastName))
            {
                ContactName = new ContactNameValueObject(contactFirstName, contactLastName);
            }
        }

        /// <summary>
        /// Updates the contact's email if their value has changed.
        /// </summary>
        /// <param name="contactEmail">The contact's new email.</param>
        public void UpdateContactEmail(string contactEmail)
        {
            if (ContactEmail.HasBeenChanged(contactEmail))
            {
                ContactEmail = new ContactEmailValueObject(contactEmail);
            }
        }

        /// <summary>
        /// Updates the contact's phone if their value has changed.
        /// </summary>
        /// <param name="contactPhone">The contact's new phone.</param>
        public void UpdateContactPhone(ContactPhoneValueObject contactPhone)
        {
            if (ContactPhone.HasBeenChanged(contactPhone.Number, contactPhone.AreaCode))
            {
                ContactPhone = contactPhone;
            }
        }
    }
}