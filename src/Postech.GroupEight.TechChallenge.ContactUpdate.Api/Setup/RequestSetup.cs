using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Requests.Context;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Requests.Context.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class RequestSetup
    {
        internal static void AddDependencyRequestCorrelationId(this IServiceCollection services)
        {
            services.AddScoped<IRequestCorrelationId, DefaultRequestCorrelationId>();
        }
    }
}