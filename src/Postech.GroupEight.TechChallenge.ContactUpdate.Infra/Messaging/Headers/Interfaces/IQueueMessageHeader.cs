namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers.Interfaces
{
    public interface IQueueMessageHeader
    {
        public Guid CorrelationId { get; }
        public Guid MessageId { get; }
        public DateTime Timestamp { get; }
        public string Source { get; }
        public string MessageType { get; }
    }
}