using Bogus;
using FluentAssertions;
using Moq;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results.Enumerators;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.ValueObjects;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Publishers;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Requests.Context;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.UnitTests.Suite.Infra.Messaging.Publishers 
{
    public class ContactUpdatedEventPublisherTests
    {
        private readonly Faker _faker = new("pt_BR");
        private readonly DefaultRequestCorrelationId _requestCorrelationId = new();  
        
        [Fact(DisplayName = "Event successfully published to integration queue")]
        [Trait("Action", "PublishEventAsync")]
        public async Task PublishEventAsync_EventSuccessfullyPublishedToIntegrationQueue_ShouldReturnResultIndicatingSuccess()
        {
            // Arrange
            ContactUpdatedEvent contactUpdatedEvent = new()
            {
                ContactId = Guid.NewGuid(),
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactPhoneNumber = ContactPhoneValueObject.Format("11", _faker.Phone.PhoneNumber("9########")),
                ContactEmail = _faker.Internet.Email()
            };
            ContactUpdatedQueueMessageHeader header = new(_requestCorrelationId.GetCorrelationId(), contactUpdatedEvent.ContactId);
            Mock<IQueue> queue = new();
            queue.Setup(q => q.PublishMessageAsync(contactUpdatedEvent, header));
            ContactUpdatedEventPublisher publisher = new(queue.Object, _requestCorrelationId);

            // Act
            PublishedEventResult result = await publisher.PublishEventAsync(contactUpdatedEvent);

            // Assert
            result.EventId.Should().Be(contactUpdatedEvent.ContactId);
            result.PublishedAt.Should().NotBeNull().And.BeOnOrBefore(DateTime.UtcNow);
            result.Status.Should().Be(PublishEventStatus.Success);
            result.Description.Should().Be("Event successfully published to integration queue");
        }

        [Fact(DisplayName = "Failed to publish event to integration queue")]
        [Trait("Action", "PublishEventAsync")]
        public async Task PublishEventAsync_FailedToPublishEventToIntegrationQueue_ShouldReturnResultIndicatingError()
        {
            // Arrange
            ContactUpdatedEvent contactUpdatedEvent = new()
            {
                ContactId = Guid.NewGuid(),
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactPhoneNumber = ContactPhoneValueObject.Format("11", _faker.Phone.PhoneNumber("9########")),
                ContactEmail = _faker.Internet.Email()
            };
            ContactUpdatedQueueMessageHeader header = new(_requestCorrelationId.GetCorrelationId(), contactUpdatedEvent.ContactId);
            Mock<IQueue> queue = new();
            queue
                .Setup(q => q.PublishMessageAsync(contactUpdatedEvent, header))
                .ThrowsAsync(new Exception("Failed to publish event to integration queue"));
            ContactUpdatedEventPublisher publisher = new(queue.Object, _requestCorrelationId);

            // Assert
            PublishedEventResult result = await publisher.PublishEventAsync(contactUpdatedEvent);

            // Assert
            result.EventId.Should().Be(contactUpdatedEvent.ContactId);
            result.PublishedAt.Should().BeNull();
            result.Status.Should().Be(PublishEventStatus.Error);
            result.Description.Should().Be("Failed to publish event to integration queue");
        }
    }
}