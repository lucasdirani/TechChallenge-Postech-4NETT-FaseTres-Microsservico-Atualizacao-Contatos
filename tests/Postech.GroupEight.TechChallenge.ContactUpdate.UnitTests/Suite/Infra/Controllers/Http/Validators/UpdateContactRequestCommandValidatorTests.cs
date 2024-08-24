using Bogus;
using FluentValidation.TestHelper;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Validators;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.UnitTests.Suite.Infra.Controllers.Http.Validators
{
    public class UpdateContactRequestCommandValidatorTests
    {
        private readonly UpdateContactRequestCommandValidator _validator;
        private readonly Faker _faker = new("pt_BR");

        public UpdateContactRequestCommandValidatorTests()
        {
            _validator = new();
        }

        [Fact(DisplayName = "All command properties are valid")]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_AllCommandPropertiesAreValid_ShouldNotHaveValidationErrors()
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                ContactName = new()
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName()
                },
                ContactEmail = new()
                {
                    Address = _faker.Internet.Email()
                },
                ContactPhone = new()
                {
                    Number = _faker.Phone.PhoneNumber("9########"),
                    AreaCode = "11"
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.ContactId);
            result.ShouldNotHaveValidationErrorFor(c => c.ContactName);
            result.ShouldNotHaveValidationErrorFor(c => c.ContactName.FirstName);
            result.ShouldNotHaveValidationErrorFor(c => c.ContactName.LastName);
            result.ShouldNotHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldNotHaveValidationErrorFor(c => c.ContactEmail.Address);
            result.ShouldNotHaveValidationErrorFor(c => c.ContactPhone);
            result.ShouldNotHaveValidationErrorFor(c => c.ContactPhone.Number);
            result.ShouldNotHaveValidationErrorFor(c => c.ContactPhone.AreaCode);
        }

        [Theory(DisplayName = "Contact first name property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("MaximilianoGustavianoLeonardocristovãoBeneditinoalberto")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_ContactFirstNamePropertyIsInvalid_ShouldHaveValidationErrors(string contactFirstName)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                ContactName = new()
                {
                    FirstName = contactFirstName,
                    LastName = _faker.Name.LastName()
                },
                ContactEmail = new()
                {
                    Address = _faker.Internet.Email()
                },
                ContactPhone = new()
                {
                    Number = _faker.Phone.PhoneNumber("9########"),
                    AreaCode = "11"
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ContactName.FirstName);
        }

        [Theory(DisplayName = "Contact last name property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("De Oliveira da Silva Monteiro dos Santos Pereira de Albuquerque e Souza")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_ContactLastNamePropertyIsInvalid_ShouldHaveValidationErrors(string contactLastName)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                ContactName = new()
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = contactLastName
                },
                ContactEmail = new()
                {
                    Address = _faker.Internet.Email()
                },
                ContactPhone = new()
                {
                    Number = _faker.Phone.PhoneNumber("9########"),
                    AreaCode = "11"
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ContactName.LastName);
        }

        [Theory(DisplayName = "Contact email address property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("nome.sobrenome.extremamente.longo.que.ultrapassa.sessenta.caracteres@email.com")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_ContactEmailAddressPropertyIsInvalid_ShouldHaveValidationErrors(string contactEmailAddress)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                ContactName = new()
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName()
                },
                ContactEmail = new()
                {
                    Address = contactEmailAddress
                },
                ContactPhone = new()
                {
                    Number = _faker.Phone.PhoneNumber("9########"),
                    AreaCode = "11"
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail.Address);
        }

        [Theory(DisplayName = "Contact phone number property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("9768542")]
        [InlineData("9768542836")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_ContactPhoneNumberPropertyIsInvalid_ShouldHaveValidationErrors(string contactPhoneNumber)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                ContactName = new()
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName()
                },
                ContactEmail = new()
                {
                    Address = _faker.Internet.Email()
                },
                ContactPhone = new()
                {
                    Number = contactPhoneNumber,
                    AreaCode = "11"
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ContactPhone.Number);
        }

        [Theory(DisplayName = "Contact phone area code property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("2")]
        [InlineData("1A")]
        [InlineData("105")]
        [InlineData("BB")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_ContactPhoneAreaCodePropertyIsInvalid_ShouldHaveValidationErrors(string contactPhoneAreaCode)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                ContactName = new()
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName()
                },
                ContactEmail = new()
                {
                    Address = _faker.Internet.Email()
                },
                ContactPhone = new()
                {
                    Number = _faker.Phone.PhoneNumber("9########"),
                    AreaCode = contactPhoneAreaCode
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ContactPhone.AreaCode);
        }

        [Fact(DisplayName = "Contact identification property is invalid")]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_ContactIdentificationPropertyIsInvalid_ShouldHaveValidationErrors()
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.Empty,
                ContactName = new()
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName()
                },
                ContactEmail = new()
                {
                    Address = _faker.Internet.Email()
                },
                ContactPhone = new()
                {
                    Number = _faker.Phone.PhoneNumber("9########"),
                    AreaCode = "11"
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ContactId);
        }
    }
}