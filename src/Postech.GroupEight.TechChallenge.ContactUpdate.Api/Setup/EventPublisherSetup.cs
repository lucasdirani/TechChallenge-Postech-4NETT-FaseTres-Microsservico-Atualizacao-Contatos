using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Publishers;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class EventPublisherSetup
    {
        internal static void AddDependencyEventPublisher(this IServiceCollection services)
        {
            services.AddScoped<IEventPublisher<ContactUpdatedEvent>, ContactUpdatedEventPublisher>();
        }
    }
}