using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Integrations.RabbitMQ.Configurations
{
    [ExcludeFromCodeCoverage]
    public record RabbitMQConnectionConfiguration
    {
        public required string HostAddress { get; init; }
        public required string HostUsername { get; init; }
        public required string HostPassword { get; init; }
        public required RabbitMQCircuitBreakerConfiguration CircuitBreakerConfiguration { get; init; }
        public required RabbitMQMessageConfiguration MessageConfiguration { get; init; }
        public required RabbitMQPublishConfiguration PublishConfiguration { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record RabbitMQMessageConfiguration
    {
        public required string EntityName { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record RabbitMQPublishConfiguration
    {
        public required string ExchangeType { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record RabbitMQCircuitBreakerConfiguration
    {
        public int TrackingPeriodInMinutes { get; init; }
        public int TripThreshold { get; init; }
        public int ActiveThreshold { get; init; }
        public int ResetIntervalInMinutes { get; init; }
    }
}