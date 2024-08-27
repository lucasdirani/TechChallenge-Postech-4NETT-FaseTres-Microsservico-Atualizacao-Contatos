using FluentValidation;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.ValueObjects;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Validators
{
    public class UpdateContactRequestCommandValidator : AbstractValidator<UpdateContactRequestCommand>
    {
        public UpdateContactRequestCommandValidator()
        {
            AddRuleForContactId();
            AddRulesForCurrentContactData();
            AddRulesForUpdatedContactData();
        }

        private void AddRulesForCurrentContactData()
        {
            AddRulesForCurrentContactName();
            AddRulesForCurrentContactEmail();
            AddRulesForCurrentContactPhone();

            void AddRulesForCurrentContactName()
            {
                RuleFor(command => command.CurrentContactData.ContactName).NotNull().NotEmpty().WithMessage("The current contact name must be provided");
                RuleFor(command => command.CurrentContactData.ContactName.FirstName).NotNull().NotEmpty().WithMessage("The current contact first name must be provided");
                RuleFor(command => command.CurrentContactData.ContactName.FirstName).MaximumLength(ContactNameValueObject.ContactFirstNameMaxLength).WithMessage($"The current contact's first name must not exceed {ContactNameValueObject.ContactFirstNameMaxLength} characters");
                RuleFor(command => command.CurrentContactData.ContactName.LastName).NotNull().NotEmpty().WithMessage("The current contact last name must be provided");
                RuleFor(command => command.CurrentContactData.ContactName.LastName).MaximumLength(ContactNameValueObject.ContactLastNameMaxLength).WithMessage($"The current contact's last name must not exceed {ContactNameValueObject.ContactLastNameMaxLength} characters");
            }

            void AddRulesForCurrentContactEmail()
            {
                RuleFor(command => command.CurrentContactData.ContactEmail).NotNull().NotEmpty().WithMessage("The current contact email must be provided");
                RuleFor(command => command.CurrentContactData.ContactEmail.Address).NotNull().NotEmpty().WithMessage("The current contact email address must be provided");
                RuleFor(command => command.CurrentContactData.ContactEmail.Address).MaximumLength(ContactEmailValueObject.ContactEmailMaxLength).WithMessage($"The current contact's email address must not exceed {ContactEmailValueObject.ContactEmailMaxLength} characters");
            }

            void AddRulesForCurrentContactPhone()
            {
                RuleFor(command => command.CurrentContactData.ContactPhone).NotNull().NotEmpty().WithMessage("The current contact phone must be provided");
                RuleFor(command => command.CurrentContactData.ContactPhone.Number).NotNull().NotEmpty().WithMessage("The current contact phone number must be provided");
                RuleFor(command => command.CurrentContactData.ContactPhone.Number).MaximumLength(ContactPhoneValueObject.ContactPhoneNumberMaxLength).WithMessage($"The current contact's phone number must not exceed {ContactPhoneValueObject.ContactPhoneNumberMaxLength} characters");
                RuleFor(command => command.CurrentContactData.ContactPhone.Number).MinimumLength(ContactPhoneValueObject.ContactPhoneNumberMinLength).WithMessage($"The current contact's phone number must have at least {ContactPhoneValueObject.ContactPhoneNumberMinLength} characters");
                RuleFor(command => command.CurrentContactData.ContactPhone.AreaCode).NotNull().NotEmpty().WithMessage("The current contact phone area code must be provided");
                RuleFor(command => command.CurrentContactData.ContactPhone.AreaCode).Matches("^\\d{2}$").WithMessage("The current contact phone area code must contain two numeric characters");
            }
        }

        private void AddRulesForUpdatedContactData()
        {
            AddRulesForUpdatedContactName();
            AddRulesForUpdatedContactEmail();
            AddRulesForUpdatedContactPhone();

            void AddRulesForUpdatedContactName()
            {
                RuleFor(command => command.UpdatedContactData.ContactName).NotNull().NotEmpty().WithMessage("The updated contact name must be provided");
                RuleFor(command => command.UpdatedContactData.ContactName.FirstName).NotNull().NotEmpty().WithMessage("The updated contact first name must be provided");
                RuleFor(command => command.UpdatedContactData.ContactName.FirstName).MaximumLength(ContactNameValueObject.ContactFirstNameMaxLength).WithMessage($"The updated contact's first name must not exceed {ContactNameValueObject.ContactFirstNameMaxLength} characters");
                RuleFor(command => command.UpdatedContactData.ContactName.LastName).NotNull().NotEmpty().WithMessage("The updated contact last name must be provided");
                RuleFor(command => command.UpdatedContactData.ContactName.LastName).MaximumLength(ContactNameValueObject.ContactLastNameMaxLength).WithMessage($"The updated contact's last name must not exceed {ContactNameValueObject.ContactLastNameMaxLength} characters");
            }

            void AddRulesForUpdatedContactEmail()
            {
                RuleFor(command => command.UpdatedContactData.ContactEmail).NotNull().NotEmpty().WithMessage("The updated contact email must be provided");
                RuleFor(command => command.UpdatedContactData.ContactEmail.Address).NotNull().NotEmpty().WithMessage("The updated contact email address must be provided");
                RuleFor(command => command.UpdatedContactData.ContactEmail.Address).MaximumLength(ContactEmailValueObject.ContactEmailMaxLength).WithMessage($"The updated contact's email address must not exceed {ContactEmailValueObject.ContactEmailMaxLength} characters");
            }

            void AddRulesForUpdatedContactPhone()
            {
                RuleFor(command => command.UpdatedContactData.ContactPhone).NotNull().NotEmpty().WithMessage("The updated contact phone must be provided");
                RuleFor(command => command.UpdatedContactData.ContactPhone.Number).NotNull().NotEmpty().WithMessage("The updated contact phone number must be provided");
                RuleFor(command => command.UpdatedContactData.ContactPhone.Number).MaximumLength(ContactPhoneValueObject.ContactPhoneNumberMaxLength).WithMessage($"The updated contact's phone number must not exceed {ContactPhoneValueObject.ContactPhoneNumberMaxLength} characters");
                RuleFor(command => command.UpdatedContactData.ContactPhone.Number).MinimumLength(ContactPhoneValueObject.ContactPhoneNumberMinLength).WithMessage($"The updated contact's phone number must have at least {ContactPhoneValueObject.ContactPhoneNumberMinLength} characters");
                RuleFor(command => command.UpdatedContactData.ContactPhone.AreaCode).NotNull().NotEmpty().WithMessage("The updated contact phone area code must be provided");
                RuleFor(command => command.UpdatedContactData.ContactPhone.AreaCode).Matches("^\\d{2}$").WithMessage("The updated contact phone area code must contain two numeric characters");
            }
        }

        private void AddRuleForContactId()
        {
            RuleFor(command => command.ContactId).NotEmpty().WithMessage("The contact identifier must be provided");
        }
    }
}