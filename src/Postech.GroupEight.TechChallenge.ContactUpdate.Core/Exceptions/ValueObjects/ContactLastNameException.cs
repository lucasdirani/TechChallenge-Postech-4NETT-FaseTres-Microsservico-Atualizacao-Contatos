using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Exceptions.Common;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Core.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class ContactLastNameException(string message, string lastNameValue) : DomainException(message)
    {
        public string LastNameValue { get; } = lastNameValue;

        public static void ThrowIfFormatIsInvalid(string argument, Regex format)
        {
            if (!format.IsMatch(argument))
            {
                throw new ContactLastNameException("The contact's last name must contain only letters (lowercase or uppercase), accents, hyphens and must not exceed sixty characters.", argument);
            }
        }
    }
}