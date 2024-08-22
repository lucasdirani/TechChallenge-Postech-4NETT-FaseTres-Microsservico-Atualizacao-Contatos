using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Extensions.Common;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Endpoints;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Integrations.RabbitMQ.Exceptions;

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

        public Uri GetEndpointAddress(QueueEndpoint queueEndpoint)
        {
            RabbitMQEndpointConfiguration endpointConfiguration = 
                Endpoints.FirstOrDefault(endpoint => endpoint.EndpointName.Equals(queueEndpoint.GetDescription(), StringComparison.OrdinalIgnoreCase)) 
                ?? throw new GetRabbitMQEndpointAddressException("The requested endpoint is not configured", queueEndpoint);
            return new($"queue:{endpointConfiguration.EndpointName}");
        }
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