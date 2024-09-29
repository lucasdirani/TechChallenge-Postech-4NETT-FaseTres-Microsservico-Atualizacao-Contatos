using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Validators;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class FluentValidationSetup
    {
        internal static void AddDependencyFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UpdateContactRequestCommandValidator>();
        }
    }
}