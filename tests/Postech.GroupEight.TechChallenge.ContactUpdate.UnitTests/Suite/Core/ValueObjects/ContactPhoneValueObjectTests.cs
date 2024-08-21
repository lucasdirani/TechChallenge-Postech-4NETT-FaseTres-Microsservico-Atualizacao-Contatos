using System.Text.RegularExpressions;
using Bogus;
using FluentAssertions;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Exceptions.ValueObjects;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.ValueObjects;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.UnitTests.Suite.Core.ValueObjects
{
    public class ContactPhoneValueObjectTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Theory(DisplayName = "Constructing a valid object of type ContactPhoneValueObject")]
        [InlineData("987654325")]
        [InlineData("987654330")]
        [InlineData("987654343")]
        [InlineData("987654363")]
        [InlineData("987654366")]
        [InlineData("987654369")]
        [InlineData("987654370")]
        [InlineData("87654337")]
        [InlineData("87654327")]
        [InlineData("87654321")]
        [Trait("Action", "ContactPhoneValueObject")]
        public void ContactPhoneValueObject_ValidData_ShouldConstructContactPhoneValueObject(string phoneNumber)
        {
            AreaCodeValueObject areaCode = AreaCodeValueObject.Create("11");
            ContactPhoneValueObject contactPhone = new(phoneNumber, areaCode);
            contactPhone.Should().NotBeNull();
            contactPhone.Number.Should().Be(phoneNumber);
            contactPhone.AreaCode.Should().Be(areaCode);
        }

        [Theory(DisplayName = "Construct an object of type ContactPhoneValueObject with an invalid phone number")]
        [InlineData("0123456789")]
        [InlineData("1122334455")]
        [InlineData("9876543200")]
        [InlineData("1111111111")]
        [InlineData("123456789012")]
        [InlineData("87654321A")]
        [InlineData("8#7654321")]
        [InlineData("(123)456-7890")]
        [InlineData("987.654.3210")]
        [InlineData("8765432@10")]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Action", "ContactPhoneValueObject")]
        public void ContactPhoneValueObject_InvalidPhoneNumber_ShouldThrowContactPhoneNumberException(string phoneNumber)
        {
            AreaCodeValueObject areaCode = AreaCodeValueObject.Create("11");
            ContactPhoneNumberException exception = Assert.Throws<ContactPhoneNumberException>(() => new ContactPhoneValueObject(phoneNumber, areaCode));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.PhoneNumber.Should().Be(phoneNumber);
        }

        [Theory(DisplayName = "Contact's phone number is being changed.")]
        [InlineData("987654325", "87654321")]
        [InlineData("987654330", "87654327")]
        [Trait("Action", "HasBeenChanged")]
        public void HasBeenChanged_ContactPhoneNumberHasBeenChanged_ShouldReturnTrue(string currentPhoneNumber, string otherPhoneNumber)
        {
            AreaCodeValueObject areaCode = AreaCodeValueObject.Create("11");
            ContactPhoneValueObject contactPhone = new(currentPhoneNumber, areaCode);
            contactPhone.HasBeenChanged(otherPhoneNumber, areaCode).Should().BeTrue();
        }

        [Theory(DisplayName = "Contact's area code phone number is being changed.")]
        [InlineData("11", "13")]
        [InlineData("21", "22")]
        [Trait("Action", "HasBeenChanged")]
        public void HasBeenChanged_ContactAreaCodePhoneNumberHasBeenChanged_ShouldReturnTrue(string currentAreaCode, string otherAreaCode)
        {
            AreaCodeValueObject areaCode = AreaCodeValueObject.Create(currentAreaCode);
            ContactPhoneValueObject contactPhone = new(_faker.Phone.PhoneNumber("9########"), areaCode);
            contactPhone.HasBeenChanged(contactPhone.Number, AreaCodeValueObject.Create(otherAreaCode)).Should().BeTrue();
        }

        [Theory(DisplayName = "Contact's phone number and area code phone number are being changed.")]
        [InlineData("987654325", "11", "87654321", "13")]
        [InlineData("987654330", "21", "87654327", "22")]
        [Trait("Action", "HasBeenChanged")]
        public void HasBeenChanged_ContactPhoneNumberAndAreaCodePhoneNumberHaveBeenChanged_ShouldReturnTrue(
            string currentPhoneNumber,
            string currentAreaCode, 
            string otherPhoneNumber,
            string otherAreaCode)
        {
            AreaCodeValueObject areaCode = AreaCodeValueObject.Create(currentAreaCode);
            ContactPhoneValueObject contactPhone = new(currentPhoneNumber, areaCode);
            contactPhone.HasBeenChanged(otherPhoneNumber, AreaCodeValueObject.Create(otherAreaCode)).Should().BeTrue();
        }

        [Theory(DisplayName = "Contact's phone number and area code phone number are not being changed.")]
        [InlineData("987654325", "11", "987654325", "11")]
        [InlineData("987654330", "21", "987654330", "21")]
        [Trait("Action", "HasBeenChanged")]
        public void HasBeenChanged_ContactPhoneNumberAndAreaCodePhoneNumberHaveNotBeenChanged_ShouldReturnFalse(
            string currentPhoneNumber,
            string currentAreaCode,
            string otherPhoneNumber,
            string otherAreaCode)
        {
            AreaCodeValueObject areaCode = AreaCodeValueObject.Create(currentAreaCode);
            ContactPhoneValueObject contactPhone = new(currentPhoneNumber, areaCode);
            contactPhone.HasBeenChanged(otherPhoneNumber, AreaCodeValueObject.Create(otherAreaCode)).Should().BeFalse();
        }

        [Theory(DisplayName = "Constructing a valid object of type ContactPhoneValueObject")]
        [InlineData("(11)987654325")]
        [InlineData("(31)987654330")]
        [InlineData("(99)987654343")]
        [InlineData("(21)987654363")]
        [InlineData("(44)987654366")]
        [InlineData("(51)987654369")]
        [InlineData("(12)987654370")]
        [InlineData("(13)87654337")]
        [InlineData("(61)87654327")]
        [InlineData("(73)87654321")]
        [Trait("Action", "Create")]
        public void Create_ValidData_ShouldConstructContactPhoneValueObject(string phoneNumberWithAreaCode)
        {
            Match phoneNumberWithAreaCodePatternMatch = Regex.Match(phoneNumberWithAreaCode, @"\((.*?)\)(\d+)");
            ContactPhoneValueObject contactPhone = ContactPhoneValueObject.Create(phoneNumberWithAreaCode);
            contactPhone.Should().NotBeNull();
            contactPhone.Number.Should().Be(phoneNumberWithAreaCodePatternMatch.Groups[2].Value);
            contactPhone.AreaCode.Value.Should().Be(phoneNumberWithAreaCodePatternMatch.Groups[1].Value);
        }

        [Fact(DisplayName = "Format a phone number with eight digits")]
        [Trait("Action", "Format")]
        public void Format_EightDigitPhoneNumber_ShouldFormatPhoneNumber()
        {
            string areaCode = "11";
            string phoneNumber = "98765432";
            string formattedPhoneNumber = ContactPhoneValueObject.Format(areaCode, phoneNumber);
            formattedPhoneNumber.Should().Be("(11) 9876-5432");
        }

        [Fact(DisplayName = "Format a phone number with nine digits")]
        [Trait("Action", "Format")]
        public void Format_NineDigitPhoneNumber_ShouldFormatPhoneNumber()
        {
            string areaCode = "21";
            string phoneNumber = "287654325";
            string formattedPhoneNumber = ContactPhoneValueObject.Format(areaCode, phoneNumber);
            formattedPhoneNumber.Should().Be("(21) 28765-4325");
        }

        [Theory(DisplayName = "Format a phone number with nine digits")]
        [InlineData("11", "0123456789")]
        [InlineData("11", "1122334455")]
        [InlineData("11", "9876543200")]
        [InlineData("11", "1111111111")]
        [InlineData("11", "123456789012")]
        [InlineData("11", "87654321A")]
        [Trait("Action", "Format")]
        public void Format_InvalidPhoneNumber_ShouldThrowContactPhoneNumberException(string areaCode, string phoneNumber)
        {
            ContactPhoneNumberException exception = Assert.Throws<ContactPhoneNumberException>(() => ContactPhoneValueObject.Format(areaCode, phoneNumber));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.PhoneNumber.Should().Be(phoneNumber);
        }

        [Theory(DisplayName = "Format a phone number with nine digits")]
        [InlineData("100", "287654325")]
        [InlineData("08", "98765432")]
        [InlineData("60", "87654337")]
        [Trait("Action", "Format")]
        public void Format_InvalidAreaCode_ShouldThrowAreaCodeValueNotSupportedException(string areaCode, string phoneNumber)
        {
            AreaCodeValueNotSupportedException exception = Assert.Throws<AreaCodeValueNotSupportedException>(() => ContactPhoneValueObject.Format(areaCode, phoneNumber));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.AreaCodeValue.Should().Be(areaCode);
        }
    }
}