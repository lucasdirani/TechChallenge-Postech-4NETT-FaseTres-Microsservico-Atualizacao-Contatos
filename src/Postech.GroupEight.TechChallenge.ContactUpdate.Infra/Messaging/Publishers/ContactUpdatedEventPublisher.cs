using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results.Enumerators;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Requests.Context.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Publishers
{
    public class ContactUpdatedEventPublisher(IQueue queue, IRequestCorrelationId requestCorrelationId) : IEventPublisher<ContactUpdatedEvent>
    {
        private readonly IQueue _queue = queue;
        private readonly IRequestCorrelationId _requestCorrelationId = requestCorrelationId;

        public async Task<PublishedEventResult> PublishEventAsync(ContactUpdatedEvent eventData)
        {
            try 
            {
                ContactUpdatedQueueMessageHeader header = new(_requestCorrelationId.GetCorrelationId(), eventData.ContactId);
                await _queue.PublishMessageAsync(eventData, header);
                return new() {
                    EventId = eventData.ContactId,
                    PublishedAt = DateTime.UtcNow,
                    Status = PublishEventStatus.Success,
                    Description = "Event successfully published to integration queue"
                };
            }
            catch (Exception ex)
            {
                return new() {
                    EventId = eventData.ContactId,
                    Status = PublishEventStatus.Error,
                    Description = ex.Message
                };
            }
        }
    }
}