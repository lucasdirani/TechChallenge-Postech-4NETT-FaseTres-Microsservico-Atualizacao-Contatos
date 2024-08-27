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
                CurrentContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.ContactId);
            result.ShouldNotHaveValidationErrorFor(c => c.CurrentContactData);
            result.ShouldNotHaveValidationErrorFor(c => c.CurrentContactData.ContactName);
            result.ShouldNotHaveValidationErrorFor(c => c.CurrentContactData.ContactName.FirstName);
            result.ShouldNotHaveValidationErrorFor(c => c.CurrentContactData.ContactName.LastName);
            result.ShouldNotHaveValidationErrorFor(c => c.CurrentContactData.ContactEmail);
            result.ShouldNotHaveValidationErrorFor(c => c.CurrentContactData.ContactEmail.Address);
            result.ShouldNotHaveValidationErrorFor(c => c.CurrentContactData.ContactPhone);
            result.ShouldNotHaveValidationErrorFor(c => c.CurrentContactData.ContactPhone.Number);
            result.ShouldNotHaveValidationErrorFor(c => c.CurrentContactData.ContactPhone.AreaCode);
            result.ShouldNotHaveValidationErrorFor(c => c.UpdatedContactData);
            result.ShouldNotHaveValidationErrorFor(c => c.UpdatedContactData.ContactName);
            result.ShouldNotHaveValidationErrorFor(c => c.UpdatedContactData.ContactName.FirstName);
            result.ShouldNotHaveValidationErrorFor(c => c.UpdatedContactData.ContactName.LastName);
            result.ShouldNotHaveValidationErrorFor(c => c.UpdatedContactData.ContactEmail);
            result.ShouldNotHaveValidationErrorFor(c => c.UpdatedContactData.ContactEmail.Address);
            result.ShouldNotHaveValidationErrorFor(c => c.UpdatedContactData.ContactPhone);
            result.ShouldNotHaveValidationErrorFor(c => c.UpdatedContactData.ContactPhone.Number);
            result.ShouldNotHaveValidationErrorFor(c => c.UpdatedContactData.ContactPhone.AreaCode);
        }

        [Fact(DisplayName = "Contact identification property is invalid")]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_ContactIdentificationPropertyIsInvalid_ShouldHaveValidationErrors()
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.Empty,
                CurrentContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ContactId);
        }

        [Theory(DisplayName = "Updated contact first name property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("MaximilianoGustavianoLeonardocristovãoBeneditinoalberto")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_UpdatedContactFirstNamePropertyIsInvalid_ShouldHaveValidationErrors(string updatedContactFirstName)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = updatedContactFirstName,
                        LastName = _faker.Name.LastName()
                    },
                    ContactEmail = new()
                    {
                        Address = _faker.Internet.Email()
                    },
                    ContactPhone = new()
                    {
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.UpdatedContactData.ContactName.FirstName);
        }

        [Theory(DisplayName = "Updated contact last name property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("De Oliveira da Silva Monteiro dos Santos Pereira de Albuquerque e Souza")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_UpdatedContactLastNamePropertyIsInvalid_ShouldHaveValidationErrors(string updatedContactLastName)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = updatedContactLastName
                    },
                    ContactEmail = new()
                    {
                        Address = _faker.Internet.Email()
                    },
                    ContactPhone = new()
                    {
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.UpdatedContactData.ContactName.LastName);
        }

        [Theory(DisplayName = "Updated contact email address property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("nome.sobrenome.extremamente.longo.que.ultrapassa.sessenta.caracteres@email.com")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_UpdatedContactEmailAddressPropertyIsInvalid_ShouldHaveValidationErrors(string updatedContactEmailAddress)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = _faker.Name.LastName()
                    },
                    ContactEmail = new()
                    {
                        Address = updatedContactEmailAddress
                    },
                    ContactPhone = new()
                    {
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.UpdatedContactData.ContactEmail.Address);
        }

        [Theory(DisplayName = "Updated contact phone number property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("9768542")]
        [InlineData("9768542836")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_UpdatedContactPhoneNumberPropertyIsInvalid_ShouldHaveValidationErrors(string updatedContactPhoneNumber)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
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
                        AreaCode = "11",
                        Number = updatedContactPhoneNumber
                    }
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.UpdatedContactData.ContactPhone.Number);
        }

        [Theory(DisplayName = "Updated contact phone area code property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("2")]
        [InlineData("1A")]
        [InlineData("105")]
        [InlineData("BB")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_UpdatedContactPhoneAreaCodePropertyIsInvalid_ShouldHaveValidationErrors(string updatedContactPhoneAreaCode)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
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
                        AreaCode = updatedContactPhoneAreaCode,
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.UpdatedContactData.ContactPhone.AreaCode);
        }

        [Theory(DisplayName = "Current contact first name property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("MaximilianoGustavianoLeonardocristovãoBeneditinoalberto")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_CurrentContactFirstNamePropertyIsInvalid_ShouldHaveValidationErrors(string currentContactFirstName)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = currentContactFirstName,
                        LastName = _faker.Name.LastName()
                    },
                    ContactEmail = new()
                    {
                        Address = _faker.Internet.Email()
                    },
                    ContactPhone = new()
                    {
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.CurrentContactData.ContactName.FirstName);
        }

        [Theory(DisplayName = "Current contact last name property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("De Oliveira da Silva Monteiro dos Santos Pereira de Albuquerque e Souza")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_CurrentContactLastNamePropertyIsInvalid_ShouldHaveValidationErrors(string currentContactLastName)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = currentContactLastName
                    },
                    ContactEmail = new()
                    {
                        Address = _faker.Internet.Email()
                    },
                    ContactPhone = new()
                    {
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.CurrentContactData.ContactName.LastName);
        }

        [Theory(DisplayName = "Current contact email address property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("nome.sobrenome.extremamente.longo.que.ultrapassa.sessenta.caracteres@email.com")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_CurrentContactEmailAddressPropertyIsInvalid_ShouldHaveValidationErrors(string currentContactEmailAddress)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = _faker.Name.LastName()
                    },
                    ContactEmail = new()
                    {
                        Address = currentContactEmailAddress
                    },
                    ContactPhone = new()
                    {
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.CurrentContactData.ContactEmail.Address);
        }

        [Theory(DisplayName = "Current contact phone number property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("9768542")]
        [InlineData("9768542836")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_CurrentContactPhoneNumberPropertyIsInvalid_ShouldHaveValidationErrors(string currentContactPhoneNumber)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
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
                        AreaCode = "11",
                        Number = currentContactPhoneNumber
                    }
                },
                UpdatedContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.CurrentContactData.ContactPhone.Number);
        }

        [Theory(DisplayName = "Current contact phone area code property is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("2")]
        [InlineData("1A")]
        [InlineData("105")]
        [InlineData("BB")]
        [InlineData(null)]
        [Trait("Action", "UpdateContactRequestCommandValidator")]
        public void UpdateContactRequestCommandValidator_CurrentContactPhoneAreaCodePropertyIsInvalid_ShouldHaveValidationErrors(string currentContactPhoneAreaCode)
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
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
                        AreaCode = currentContactPhoneAreaCode,
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
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
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            TestValidationResult<UpdateContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.CurrentContactData.ContactPhone.AreaCode);
        }
    }
}