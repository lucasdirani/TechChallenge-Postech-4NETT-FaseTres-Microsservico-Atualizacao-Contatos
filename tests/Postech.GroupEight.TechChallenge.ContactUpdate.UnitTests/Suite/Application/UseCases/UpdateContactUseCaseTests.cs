using Bogus;
using FluentAssertions;
using Moq;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Inputs;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Outputs;
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
                    PublishedAt = contactEventPublishedAt
                });
            UpdateContactUseCase useCase = new(eventPublisher.Object);

            // Act
            UpdateContactOutput updateContactOutput = await useCase.ExecuteAsync(updateContactInput);

            // Assert
            updateContactOutput.ContactId.Should().Be(contactId);
            updateContactOutput.ContactNotifiedForUpdateAt.Should().Be(contactEventPublishedAt);
            updateContactOutput.ContactFirstName.Should().Be(updatedContactDataInput.ContactFirstName);
            updateContactOutput.ContactLastName.Should().Be(updatedContactDataInput.ContactLastName);
            updateContactOutput.ContactEmail.Should().Be(updatedContactDataInput.ContactEmail);
            updateContactOutput.ContactPhoneNumber.Should().Be(ContactPhoneValueObject.Format(updatedContactDataInput.ContactPhoneNumberAreaCode, updatedContactDataInput.ContactPhoneNumber));
            eventPublisher.Verify(e => e.PublishEventAsync(It.Is<ContactUpdatedEvent>(c => c.ContactId.Equals(contactId))), Times.Once());
        }
    }
}