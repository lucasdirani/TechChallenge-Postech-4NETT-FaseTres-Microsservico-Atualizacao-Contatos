using System.Text.RegularExpressions;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Exceptions.ValueObjects;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Core.ValueObjects
{
    public partial record ContactPhoneValueObject
    {
        public ContactPhoneValueObject(string phoneNumber, AreaCodeValueObject areaCode)
            : this(phoneNumber)
        {
            AreaCode = areaCode;
        }

        private ContactPhoneValueObject(string number)
        {
            ContactPhoneNumberException.ThrowIfFormatIsInvalid(number, PhoneNumberRegex());
            Number = number;
        }

        public static ContactPhoneValueObject Create(string phoneNumberWithAreaCode)
        {
            string phoneNumberWithAreaCodePattern = @"\((.*?)\)(\d+)";
            Match phoneNumberWithAreaCodePatternMatch = Regex.Match(phoneNumberWithAreaCode, phoneNumberWithAreaCodePattern);
            string areaCode = phoneNumberWithAreaCodePatternMatch.Groups[1].Value;
            string phoneNumber = phoneNumberWithAreaCodePatternMatch.Groups[2].Value;
            return new(phoneNumber, AreaCodeValueObject.Create(areaCode));
        }

        public static string Format(string phoneNumberAreaCode, string phoneNumber)
        {
            if (phoneNumber.Length == 8)
            {
                return $"({phoneNumberAreaCode}) {phoneNumber[..4]}-{phoneNumber[4..]}";
            }
            else if (phoneNumber.Length == 9)
            {
                return $"({phoneNumberAreaCode}) {phoneNumber[..5]}-{phoneNumber[5..]}";
            }
            return string.Empty;
        }

        public string Number { get; init; }
        public AreaCodeValueObject AreaCode { get; init; }

        [GeneratedRegex("^[2-9][0-9]{3,4}[0-9]{4}$", RegexOptions.Compiled)]
        private static partial Regex PhoneNumberRegex();

        /// <summary>
        /// Indicates whether the phone number or area code has different values.
        /// </summary>
        /// <param name="otherNumber">The new number that will be used as a comparison for the current number.</param>
        /// <param name="otherAreaCode">The new area code that will be used as a comparison for the current area code.</param>
        /// <returns>Returns true if the number or area code has been changed. Otherwise, it returns false.</returns>
        public bool HasBeenChanged(string otherNumber, AreaCodeValueObject otherAreaCode)
        {
            return Number != otherNumber || AreaCode != otherAreaCode;
        }
    }
}