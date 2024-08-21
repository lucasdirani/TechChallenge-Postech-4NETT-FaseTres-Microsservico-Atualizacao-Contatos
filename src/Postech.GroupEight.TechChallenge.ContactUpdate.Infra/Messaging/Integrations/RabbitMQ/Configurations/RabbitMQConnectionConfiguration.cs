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
        public required IEnumerable<RabbitMQEndpointConfiguration> Endpoints { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record RabbitMQEndpointConfiguration
    {
        public required string EndpointName { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record RabbitMQCircuitBreakerConfiguration
    {
        public int TrackingPeriodInMinutes { get; init; }
        public double TripThreshold { get; init; }
        public int ActiveThreshold { get; init; }
        public int ResetIntervalInMinutes { get; init; }
    }
}