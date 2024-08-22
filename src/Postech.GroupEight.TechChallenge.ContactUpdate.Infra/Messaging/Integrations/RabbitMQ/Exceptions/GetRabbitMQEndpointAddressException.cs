using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Endpoints;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Integrations.RabbitMQ.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class GetRabbitMQEndpointAddressException(string? message, QueueEndpoint queueEndpoint) : Exception(message)
    {
        public QueueEndpoint QueueEndpoint { get; private set; } = queueEndpoint;
    }
}