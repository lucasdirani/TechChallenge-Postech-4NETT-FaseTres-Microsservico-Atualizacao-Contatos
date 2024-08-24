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
            AddRuleForContactName();
            AddRuleForContactEmail();
            AddRuleForContactPhone();
        }

        private void AddRuleForContactPhone()
        {
            RuleFor(command => command.ContactPhone).NotNull().NotEmpty().WithMessage("The contact phone must be provided");
            RuleFor(command => command.ContactPhone.Number).NotNull().NotEmpty().WithMessage("The contact phone number must be provided");
            RuleFor(command => command.ContactPhone.Number).MaximumLength(ContactPhoneValueObject.ContactPhoneNumberMaxLength).WithMessage($"The contact's phone number must not exceed {ContactPhoneValueObject.ContactPhoneNumberMaxLength} characters");
            RuleFor(command => command.ContactPhone.Number).MinimumLength(ContactPhoneValueObject.ContactPhoneNumberMinLength).WithMessage($"The contact's phone number must have at least {ContactPhoneValueObject.ContactPhoneNumberMinLength} characters");
            RuleFor(command => command.ContactPhone.AreaCode).NotNull().NotEmpty().WithMessage("The contact phone area code must be provided");
            RuleFor(command => command.ContactPhone.AreaCode).Matches("^\\d{2}$").WithMessage("The contact phone area code must contain two numeric characters");
        }

        private void AddRuleForContactEmail()
        {
            RuleFor(command => command.ContactEmail).NotNull().NotEmpty().WithMessage("The contact email must be provided");
            RuleFor(command => command.ContactEmail.Address).NotNull().NotEmpty().WithMessage("The contact email address must be provided");
            RuleFor(command => command.ContactEmail.Address).MaximumLength(ContactEmailValueObject.ContactEmailMaxLength).WithMessage($"The contact's email address must not exceed {ContactEmailValueObject.ContactEmailMaxLength} characters");
        }

        private void AddRuleForContactName()
        {
            RuleFor(command => command.ContactName).NotNull().NotEmpty().WithMessage("The contact name must be provided");
            RuleFor(command => command.ContactName.FirstName).NotNull().NotEmpty().WithMessage("The contact first name must be provided");
            RuleFor(command => command.ContactName.FirstName).MaximumLength(ContactNameValueObject.ContactFirstNameMaxLength).WithMessage($"The contact's first name must not exceed {ContactNameValueObject.ContactFirstNameMaxLength} characters");
            RuleFor(command => command.ContactName.LastName).NotNull().NotEmpty().WithMessage("The contact last name must be provided");
            RuleFor(command => command.ContactName.LastName).MaximumLength(ContactNameValueObject.ContactLastNameMaxLength).WithMessage($"The contact's last name must not exceed {ContactNameValueObject.ContactLastNameMaxLength} characters");
        }

        private void AddRuleForContactId()
        {
            RuleFor(command => command.ContactId).NotEmpty().WithMessage("The contact identifier must be provided");
        }
    }
}