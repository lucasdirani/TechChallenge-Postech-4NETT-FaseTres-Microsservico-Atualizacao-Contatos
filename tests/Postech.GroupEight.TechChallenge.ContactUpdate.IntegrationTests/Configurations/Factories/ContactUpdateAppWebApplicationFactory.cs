using System.Diagnostics.CodeAnalysis;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Integrations.RabbitMQ.Configurations;
using Postech.GroupEight.TechChallenge.ContactUpdate.IntegrationTests.Configurations.Factories.Extensions;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.IntegrationTests.Configurations.Factories
{
    [ExcludeFromCodeCoverage]
    public class ContactUpdateAppWebApplicationFactory(string rabbitMqConnectionString) : WebApplicationFactory<Program>
    {
        private readonly string _rabbitMqConnectionString = rabbitMqConnectionString;
        private IConfiguration? _configuration = null;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
                configurationBuilder.AddJsonFile("appsettings.IntegrationTests.json");
                _configuration = configurationBuilder.Build();
            });
            builder.ConfigureServices(services =>
            {
                List<ServiceDescriptor> massTransitServiceDescriptors = services.GetMassTransitServiceDescriptors();
                foreach (ServiceDescriptor massTransitServiceDescriptor in massTransitServiceDescriptors)
                {
                    services.Remove(massTransitServiceDescriptor);
                }
                RabbitMQConnectionConfiguration? connectionConfiguration = _configuration?.GetSection(nameof(RabbitMQConnectionConfiguration)).Get<RabbitMQConnectionConfiguration>();
                ArgumentNullException.ThrowIfNull(connectionConfiguration);
                services.AddMassTransit(m =>
                {
                    m.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(_rabbitMqConnectionString, h =>
                        {
                            h.Username(connectionConfiguration.HostUsername);
                            h.Password(connectionConfiguration.HostPassword);
                        });
                        cfg.Message<ContactUpdatedEvent>(m => 
                        {
                            m.SetEntityName(connectionConfiguration.MessageConfiguration.EntityName);
                        });
                        cfg.Publish<ContactUpdatedEvent>(p => 
                        {
                            p.ExchangeType = connectionConfiguration.PublishConfiguration.ExchangeType;
                        });
                        cfg.Send<ContactUpdatedEvent>(s =>
                        {
                            s.UseRoutingKeyFormatter(context => context.Message.EventType);
                        });
                        cfg.UseCircuitBreaker(cb =>
                        {
                            cb.TrackingPeriod = TimeSpan.FromMinutes(connectionConfiguration.CircuitBreakerConfiguration.TrackingPeriodInMinutes);
                            cb.TripThreshold = connectionConfiguration.CircuitBreakerConfiguration.TripThreshold;
                            cb.ActiveThreshold = connectionConfiguration.CircuitBreakerConfiguration.ActiveThreshold;
                            cb.ResetInterval = TimeSpan.FromMinutes(connectionConfiguration.CircuitBreakerConfiguration.ResetIntervalInMinutes); 
                        });
                    });
                });
            });
        }
    }
}