using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class UseCaseSetup
    {
        internal static void AddDependencyUseCase(this IServiceCollection services)
        {
            services.AddScoped<IUpdateContactUseCase, UpdateContactUseCase>();
        }
    }
}