using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Core.Exceptions.Common
{
    [ExcludeFromCodeCoverage]
    public class DomainException(string message) : Exception(message)
    {
        public static void ThrowWhen(bool invalidRule, string message)
        {
            if (invalidRule)
            {
                throw new DomainException(message);
            }
        }

        public static void ThrowWhenThereAreErrorMessages(IEnumerable<ValidationResult> validationResults)
        {
            if (validationResults is not null && validationResults.Any())
            {
                ValidationResult validationResult = validationResults.ElementAt(0);
                string errorMessage = validationResult?.ErrorMessage ?? string.Empty;
                throw new DomainException(errorMessage);
            }              
        }
    }
}