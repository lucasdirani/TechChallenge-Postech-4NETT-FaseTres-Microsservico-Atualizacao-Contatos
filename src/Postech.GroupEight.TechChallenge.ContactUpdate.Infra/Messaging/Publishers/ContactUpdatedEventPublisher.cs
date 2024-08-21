using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results.Enumerators;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Publishers
{
    public class ContactUpdatedEventPublisher(IQueue queue) : IEventPublisher<ContactUpdatedEvent>
    {
        private readonly IQueue _queue = queue;

        public async Task<PublishedEventResult> PublishEventAsync(ContactUpdatedEvent eventData)
        {
            await _queue.PublishMessageAsync(eventData);
            return new() {
                EventId = eventData.ContactId,
                PublishedAt = DateTime.UtcNow,
                Status = PublishEventStatus.Success
            };
        }
    }
}