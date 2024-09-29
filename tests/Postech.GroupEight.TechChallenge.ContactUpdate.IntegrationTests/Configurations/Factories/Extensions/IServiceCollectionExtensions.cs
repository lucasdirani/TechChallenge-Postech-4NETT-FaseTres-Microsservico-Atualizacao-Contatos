using System.Diagnostics.CodeAnalysis;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.IntegrationTests.Configurations.Factories.Extensions
{
    [ExcludeFromCodeCoverage]
    internal static class IServiceCollectionExtensions
    {
        internal static List<ServiceDescriptor> GetMassTransitServiceDescriptors(this IServiceCollection services)
        {
            Type[] massTransitServiceTypes =
            [
                typeof(IBusControl),
                typeof(IBus),
                typeof(IHostedService),
                typeof(ISendEndpointProvider),
                typeof(IPublishEndpoint),
                typeof(IConsumer<>)
            ];
            return services
                    .Where(s => massTransitServiceTypes.Contains(s.ServiceType) || s.ServiceType?.FullName?.Contains("MassTransit") == true)
                    .ToList();
        }
    }
}