using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers.Constants
{
    [ExcludeFromCodeCoverage]
    public static class QueueMessageHeaderName
    {
        public const string CorrelationId = "correlationId";
        public const string MessageId = "messageId";
        public const string MessageType = "messageType";
        public const string Source = "source";
        public const string Timestamp = "timestamp";
    }
}