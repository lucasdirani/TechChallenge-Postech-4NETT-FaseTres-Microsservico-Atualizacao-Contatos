using Bogus;
using FluentAssertions;
using Moq;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results.Enumerators;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Exceptions;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Inputs;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Outputs;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Exceptions.ValueObjects;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.ValueObjects;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.UnitTests.Suite.Application.UseCases 
{
    public class UpdateContactUseCaseTests
    {
        private readonly Faker _faker = new("pt_BR");
        
        [Fact(DisplayName = "Update data for a valid contact")]
        [Trait("Action", "ExecuteAsync")]
        public async Task ExecuteAsync_UpdateDataForValidContact_ShouldNotifyAboutContactUpdate()
        {
            // Arrange              
            Guid contactId = Guid.NewGuid();
            CurrentContactDataInput currentContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "11"
            };
            UpdatedContactDataInput updatedContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "21"
            };
            UpdateContactInput updateContactInput = new() 
            { 
                ContactId = contactId,
                CurrentContactData = currentContactDataInput,
                UpdatedContactData = updatedContactDataInput
            };
            Mock<IEventPublisher<ContactUpdatedEvent>> eventPublisher = new();    
            DateTime contactEventPublishedAt = new(2024, 8, 19, 12, 0, 0, DateTimeKind.Local);   
            eventPublisher
                .Setup(e => e.PublishEventAsync(new ContactUpdatedEvent()
                {
                    ContactId = contactId,
                    ContactFirstName = updatedContactDataInput.ContactFirstName,
                    ContactLastName = updatedContactDataInput.ContactLastName,
                    ContactEmail = updatedContactDataInput.ContactEmail,
                    ContactPhoneNumber = ContactPhoneValueObject.Format(updatedContactDataInput.ContactPhoneNumberAreaCode, updatedContactDataInput.ContactPhoneNumber),
                }))
                .ReturnsAsync(() => new PublishedEventResult()
                {
                    EventId = contactId,
                    PublishedAt = contactEventPublishedAt,
                    Description = "Event successfully published to integration queue"
                });
            UpdateContactUseCase useCase = new(eventPublisher.Object);

            // Act
            UpdateContactOutput updateContactOutput = await useCase.ExecuteAsync(updateContactInput);

            // Assert
            updateContactOutput.ContactId.Should().Be(contactId);
            updateContactOutput.IsContactNotifiedForUpdate.Should().BeTrue();
            updateContactOutput.ContactNotifiedForUpdateAt.Should().Be(contactEventPublishedAt);
            updateContactOutput.ContactFirstName.Should().Be(updatedContactDataInput.ContactFirstName);
            updateContactOutput.ContactLastName.Should().Be(updatedContactDataInput.ContactLastName);
            updateContactOutput.ContactEmail.Should().Be(updatedContactDataInput.ContactEmail);
            updateContactOutput.ContactPhoneNumber.Should().Be(ContactPhoneValueObject.Format(updatedContactDataInput.ContactPhoneNumberAreaCode, updatedContactDataInput.ContactPhoneNumber));
            eventPublisher.Verify(e => e.PublishEventAsync(It.Is<ContactUpdatedEvent>(c => c.ContactId.Equals(contactId))), Times.Once());
        }

        [Theory(DisplayName = "Updating a contact with a new first name in an invalid format")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("B54no")]
        [InlineData("L!c?s")]
        [InlineData("Ta*tiana")]
        [Trait("Action", "ExecuteAsync")]
        public async Task ExecuteAsync_UpdatingContactWithNewFirstNameInAnInvalidFormat_ShouldThrowContactFirstNameException(string newInvalidFirstName)
        {
            // Arrange              
            Guid contactId = Guid.NewGuid();
            CurrentContactDataInput currentContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "84"
            };
            UpdatedContactDataInput updatedContactDataInput = new() 
            { 
                ContactFirstName = newInvalidFirstName,
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "21"
            };
            UpdateContactInput updateContactInput = new() 
            { 
                ContactId = contactId,
                CurrentContactData = currentContactDataInput,
                UpdatedContactData = updatedContactDataInput
            };
            Mock<IEventPublisher<ContactUpdatedEvent>> eventPublisher = new();
            UpdateContactUseCase useCase = new(eventPublisher.Object);

            // Assert
            ContactFirstNameException exception = await Assert.ThrowsAsync<ContactFirstNameException>(() => useCase.ExecuteAsync(updateContactInput));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.FirstNameValue.Should().Be(newInvalidFirstName);
            eventPublisher.Verify(e => e.PublishEventAsync(It.Is<ContactUpdatedEvent>(c => c.ContactId.Equals(contactId))), Times.Never());
        }

        [Theory(DisplayName = "Updating a contact with a new last name in an invalid format")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Jh!ffe?son")]
        [InlineData("[ontarroy@s")]
        [InlineData( "Lim,")]
        [InlineData("Silva ")]
        [InlineData("Alves Gom^s")]
        [Trait("Action", "ExecuteAsync")]
        public async Task ExecuteAsync_UpdatingContactWithNewLastNameInAnInvalidFormat_ShouldThrowContactLastNameException(string newInvalidLastName)
        {
            // Arrange              
            Guid contactId = Guid.NewGuid();
            CurrentContactDataInput currentContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "84"
            };
            UpdatedContactDataInput updatedContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = newInvalidLastName,
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "21"
            };
            UpdateContactInput updateContactInput = new() 
            { 
                ContactId = contactId,
                CurrentContactData = currentContactDataInput,
                UpdatedContactData = updatedContactDataInput
            };
            Mock<IEventPublisher<ContactUpdatedEvent>> eventPublisher = new();
            UpdateContactUseCase useCase = new(eventPublisher.Object);

            // Assert
            ContactLastNameException exception = await Assert.ThrowsAsync<ContactLastNameException>(() => useCase.ExecuteAsync(updateContactInput));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.LastNameValue.Should().Be(newInvalidLastName);
            eventPublisher.Verify(e => e.PublishEventAsync(It.Is<ContactUpdatedEvent>(c => c.ContactId.Equals(contactId))), Times.Never());
        }

        [Theory(DisplayName = "Updating a contact with a new email address in an invalid format")]
        [InlineData("cleiton dias@gmail.com")]
        [InlineData("jair.raposo@")]
        [InlineData("milton.morgado4@hotmail")]
        [InlineData("leticia-mariagmail.com")]
        [InlineData("@yahoo.com")]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Action", "ExecuteAsync")]
        public async Task ExecuteAsync_UpdatingContactWithNewEmailAddressInAnInvalidFormat_ShouldThrowContactEmailAddressException(string newInvalidEmailAddress)
        {
            // Arrange              
            Guid contactId = Guid.NewGuid();
            CurrentContactDataInput currentContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "21"
            };
            UpdatedContactDataInput updatedContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = newInvalidEmailAddress,
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "21"
            };
            UpdateContactInput updateContactInput = new() 
            { 
                ContactId = contactId,
                CurrentContactData = currentContactDataInput,
                UpdatedContactData = updatedContactDataInput
            };
            Mock<IEventPublisher<ContactUpdatedEvent>> eventPublisher = new();
            UpdateContactUseCase useCase = new(eventPublisher.Object);

            // Assert
            ContactEmailAddressException exception = await Assert.ThrowsAsync<ContactEmailAddressException>(() => useCase.ExecuteAsync(updateContactInput));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.EmailAddressValue.Should().Be(newInvalidEmailAddress);
            eventPublisher.Verify(e => e.PublishEventAsync(It.Is<ContactUpdatedEvent>(c => c.ContactId.Equals(contactId))), Times.Never());
        }

        [Theory(DisplayName = "Updating a contact with a new phone number in an invalid format")]
        [InlineData("0123456789")]
        [InlineData("1122334455")]
        [InlineData("9876543200")]
        [InlineData("1111111111")]
        [InlineData("123456789012")]
        [InlineData("87654321A")]
        [InlineData("8#7654321")]
        [InlineData("(123)456-7890")]
        [InlineData("987.654.3210")]
        [InlineData("8765432@10")]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Action", "ExecuteAsync")]
        public async Task ExecuteAsync_UpdatingContactWithNewPhoneNumberInAnInvalidFormat_ShouldThrowContactPhoneNumberException(string newInvalidPhoneNumber)
        {
            // Arrange              
            Guid contactId = Guid.NewGuid();
            CurrentContactDataInput currentContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "21"
            };
            UpdatedContactDataInput updatedContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = newInvalidPhoneNumber,
                ContactPhoneNumberAreaCode = "99"
            };
            UpdateContactInput updateContactInput = new() 
            { 
                ContactId = contactId,
                CurrentContactData = currentContactDataInput,
                UpdatedContactData = updatedContactDataInput
            };
            Mock<IEventPublisher<ContactUpdatedEvent>> eventPublisher = new();
            UpdateContactUseCase useCase = new(eventPublisher.Object);

            // Assert
            ContactPhoneNumberException exception = await Assert.ThrowsAsync<ContactPhoneNumberException>(() => useCase.ExecuteAsync(updateContactInput));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.PhoneNumber.Should().Be(newInvalidPhoneNumber);
            eventPublisher.Verify(e => e.PublishEventAsync(It.Is<ContactUpdatedEvent>(c => c.ContactId.Equals(contactId))), Times.Never());
        }

        [Theory(DisplayName = "Updating a contact with a new area code phone number in an invalid format")]
        [InlineData("100")]
        [InlineData("09")]
        [InlineData("56")]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Action", "ExecuteAsync")]
        public async Task ExecuteAsync_UpdatingContactWithNewAreaCodePhoneNumberInAnInvalidFormat_ShouldThrowAreaCodeValueNotSupportedException(string newInvalidAreaCodePhoneNumber)
        {
            // Arrange              
            Guid contactId = Guid.NewGuid();
            CurrentContactDataInput currentContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "21"
            };
            UpdatedContactDataInput updatedContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = newInvalidAreaCodePhoneNumber
            };
            UpdateContactInput updateContactInput = new() 
            { 
                ContactId = contactId,
                CurrentContactData = currentContactDataInput,
                UpdatedContactData = updatedContactDataInput
            };
            Mock<IEventPublisher<ContactUpdatedEvent>> eventPublisher = new();
            UpdateContactUseCase useCase = new(eventPublisher.Object);

            // Assert
            AreaCodeValueNotSupportedException exception = await Assert.ThrowsAsync<AreaCodeValueNotSupportedException>(() => useCase.ExecuteAsync(updateContactInput));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.AreaCodeValue.Should().Be(newInvalidAreaCodePhoneNumber);
            eventPublisher.Verify(e => e.PublishEventAsync(It.Is<ContactUpdatedEvent>(c => c.ContactId.Equals(contactId))), Times.Never());
        }

        [Fact(DisplayName = "Updating a contact with the same data currently registered")]
        [Trait("Action", "ExecuteAsync")]
        public async Task ExecuteAsync_UpdatingContactWithTheSameDataCurrentlyRegistered_ShouldThrowUpdateContactInputException()
        {
            // Arrange              
            Guid contactId = Guid.NewGuid();
            CurrentContactDataInput currentContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "21"
            };
            UpdatedContactDataInput updatedContactDataInput = new() 
            { 
                ContactFirstName = currentContactDataInput.ContactFirstName,
                ContactLastName = currentContactDataInput.ContactLastName,
                ContactEmail = currentContactDataInput.ContactEmail,
                ContactPhoneNumber = currentContactDataInput.ContactPhoneNumber,
                ContactPhoneNumberAreaCode = currentContactDataInput.ContactPhoneNumberAreaCode
            };
            UpdateContactInput updateContactInput = new() 
            { 
                ContactId = contactId,
                CurrentContactData = currentContactDataInput,
                UpdatedContactData = updatedContactDataInput
            };
            Mock<IEventPublisher<ContactUpdatedEvent>> eventPublisher = new();
            UpdateContactUseCase useCase = new(eventPublisher.Object);

            // Assert
            UpdateContactInputException exception = await Assert.ThrowsAsync<UpdateContactInputException>(() => useCase.ExecuteAsync(updateContactInput));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.CurrentContactData.Should().Be(currentContactDataInput);
            exception.UpdatedContactData.Should().Be(updatedContactDataInput);
            eventPublisher.Verify(e => e.PublishEventAsync(It.Is<ContactUpdatedEvent>(c => c.ContactId.Equals(contactId))), Times.Never());
        }

        [Fact(DisplayName = "Failure to notify contact update")]
        [Trait("Action", "ExecuteAsync")]
        public async Task ExecuteAsync_FailureToNotifyContactUpdate_ShouldReturnOutputResultIndicatingContactUpdateNotificationFailure()
        {
            // Arrange              
            Guid contactId = Guid.NewGuid();
            CurrentContactDataInput currentContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "11"
            };
            UpdatedContactDataInput updatedContactDataInput = new() 
            { 
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "21"
            };
            UpdateContactInput updateContactInput = new() 
            { 
                ContactId = contactId,
                CurrentContactData = currentContactDataInput,
                UpdatedContactData = updatedContactDataInput
            };
            Mock<IEventPublisher<ContactUpdatedEvent>> eventPublisher = new();    
            eventPublisher
                .Setup(e => e.PublishEventAsync(new ContactUpdatedEvent()
                {
                    ContactId = contactId,
                    ContactFirstName = updatedContactDataInput.ContactFirstName,
                    ContactLastName = updatedContactDataInput.ContactLastName,
                    ContactEmail = updatedContactDataInput.ContactEmail,
                    ContactPhoneNumber = ContactPhoneValueObject.Format(updatedContactDataInput.ContactPhoneNumberAreaCode, updatedContactDataInput.ContactPhoneNumber),
                }))
                .ReturnsAsync(() => new PublishedEventResult()
                {
                    EventId = contactId,
                    Status = PublishEventStatus.Error,
                    Description = "Failed to publish event to integration queue"
                });
            UpdateContactUseCase useCase = new(eventPublisher.Object);

            // Act
            UpdateContactOutput updateContactOutput = await useCase.ExecuteAsync(updateContactInput);

            // Assert
            updateContactOutput.ContactId.Should().Be(contactId);
            updateContactOutput.IsContactNotifiedForUpdate.Should().BeFalse();
            updateContactOutput.ContactNotifiedForUpdateAt.Should().BeNull();
            updateContactOutput.ContactFirstName.Should().Be(updatedContactDataInput.ContactFirstName);
            updateContactOutput.ContactLastName.Should().Be(updatedContactDataInput.ContactLastName);
            updateContactOutput.ContactEmail.Should().Be(updatedContactDataInput.ContactEmail);
            updateContactOutput.ContactPhoneNumber.Should().Be(ContactPhoneValueObject.Format(updatedContactDataInput.ContactPhoneNumberAreaCode, updatedContactDataInput.ContactPhoneNumber));
            eventPublisher.Verify(e => e.PublishEventAsync(It.Is<ContactUpdatedEvent>(c => c.ContactId.Equals(contactId))), Times.Once());
        }
    }
}