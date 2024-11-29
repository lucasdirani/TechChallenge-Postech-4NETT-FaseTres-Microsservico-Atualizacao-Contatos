using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Api.HealthChecks;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Integrations.RabbitMQ.Configurations;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class HealthCheckSetup
    {
        internal static void AddRabbitMQHealthCheck(this IHealthChecksBuilder healthChecks)
        {
            healthChecks.AddCheck<MassTransitRabbitMqHealthCheck>(nameof(RabbitMQConnectionConfiguration));
        }
    }
}