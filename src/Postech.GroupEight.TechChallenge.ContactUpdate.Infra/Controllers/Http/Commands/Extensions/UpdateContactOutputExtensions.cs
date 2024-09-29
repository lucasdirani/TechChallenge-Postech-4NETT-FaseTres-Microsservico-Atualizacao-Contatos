using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Outputs;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands.Extensions
{
    [ExcludeFromCodeCoverage]
    internal static class UpdateContactOutputExtensions
    {
        public static UpdateContactResponseCommand AsUpdateContactResponseCommand(this UpdateContactOutput updateContactOutput)
        {
            return new()
            {
                ContactId = updateContactOutput.ContactId,
                ContactNotifiedForUpdateAt = updateContactOutput.ContactNotifiedForUpdateAt,
                IsContactNotifiedForUpdate = updateContactOutput.IsContactNotifiedForUpdate,
            };
        }
    }
}