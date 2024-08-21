using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers
{
    [ExcludeFromCodeCoverage]
    public class ContactUpdatedQueueMessageHeader(
        Guid correlationId,
        Guid messageId) 
        : IQueueMessageHeader
    {
        public Guid CorrelationId { get; } = correlationId;

        public Guid MessageId { get; } = messageId;

        public DateTime Timestamp { get; } = DateTime.UtcNow;

        public string Source { get; } = "contact.update.microservice";

        public string MessageType { get; } = nameof(ContactUpdatedEvent);

        public override bool Equals(object? obj)
        {
            return obj is ContactUpdatedQueueMessageHeader header && MessageId.Equals(header.MessageId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MessageId);
        }
    }
}