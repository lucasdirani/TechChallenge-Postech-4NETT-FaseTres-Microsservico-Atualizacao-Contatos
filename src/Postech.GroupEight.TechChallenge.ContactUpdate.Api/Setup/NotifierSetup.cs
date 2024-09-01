using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class NotifierSetup
    {
        internal static void AddDependencyNotifier(this IServiceCollection services)
        {
            services.AddScoped<INotifier, DefaultNotifier>();
        }
    }
}