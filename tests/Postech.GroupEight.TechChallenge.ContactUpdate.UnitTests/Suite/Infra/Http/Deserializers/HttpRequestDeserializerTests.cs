using System.Text.Json;
using System.Xml.Serialization;
using Bogus;
using FluentAssertions;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Deserializers;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Deserializers.Exceptions;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.UnitTests.Suite.Infra.Http.Deserializers
{
    public class HttpRequestDeserializerTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Fact(DisplayName = "Deserialize a valid request command in application json format")]
        [Trait("Action", "Deserialize")]
        public void Deserialize_DeserializeValidRequestCommandInApplicationJsonFormat_ShouldDeserializeRequestCommand()
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = _faker.Name.LastName()
                    },
                    ContactEmail = new()
                    {
                        Address = _faker.Internet.Email()
                    },
                    ContactPhone = new()
                    {
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = _faker.Name.LastName()
                    },
                    ContactEmail = new()
                    {
                        Address = _faker.Internet.Email()
                    },
                    ContactPhone = new()
                    {
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };
            string requestBody = JsonSerializer.Serialize(command);
            string format = "application/json";

            // Act
            UpdateContactRequestCommand? deserializedCommand = HttpRequestDeserializer.Deserialize<UpdateContactRequestCommand>(requestBody, format);

            // Assert
            deserializedCommand.Should().NotBeNull();
            deserializedCommand.Should().Be(command);
        }

        [Fact(DisplayName = "Deserialize a valid request command into an unsupported format")]
        [Trait("Action", "Deserialize")]
        public void Deserialize_DeserializeValidRequestCommandIntoAnUnsupportedFormat_ShouldThrowHttpResponseDeserializerException()
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = _faker.Name.LastName()
                    },
                    ContactEmail = new()
                    {
                        Address = _faker.Internet.Email()
                    },
                    ContactPhone = new()
                    {
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = _faker.Name.LastName()
                    },
                    ContactEmail = new()
                    {
                        Address = _faker.Internet.Email()
                    },
                    ContactPhone = new()
                    {
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };
            XmlSerializer xmlSerializer = new(typeof(UpdateContactRequestCommand));
            using StringWriter stringWriter = new();
            xmlSerializer.Serialize(stringWriter, command);
            string requestBody = stringWriter.ToString();
            string unsupportedFormat = "application/xml";

            // Assert
            HttpResponseDeserializerException exception = Assert.Throws<HttpResponseDeserializerException>(() => HttpRequestDeserializer.Deserialize<UpdateContactRequestCommand>(requestBody, unsupportedFormat));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.ContentType.Should().Be(unsupportedFormat);
        }

        [Fact(DisplayName = "Request body not provided")]
        [Trait("Action", "Deserialize")]
        public void Deserialize_RequestBodyNotProvided_ShouldReturnCommandDefaultInstance()
        {
            // Arrange
            string requestBody = string.Empty;
            string format = "application/json";

            // Act
            UpdateContactRequestCommand? deserializedCommand = HttpRequestDeserializer.Deserialize<UpdateContactRequestCommand>(requestBody, format);

            // Assert
            deserializedCommand.Should().Be(default(UpdateContactRequestCommand));
        }

        [Fact(DisplayName = "Content type not provided")]
        [Trait("Action", "Deserialize")]
        public void Deserialize_ContentTypeNotProvided_ShouldThrowHttpResponseDeserializerException()
        {
            // Arrange
            UpdateContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid(),
                CurrentContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = _faker.Name.LastName()
                    },
                    ContactEmail = new()
                    {
                        Address = _faker.Internet.Email()
                    },
                    ContactPhone = new()
                    {
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                },
                UpdatedContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = _faker.Name.LastName()
                    },
                    ContactEmail = new()
                    {
                        Address = _faker.Internet.Email()
                    },
                    ContactPhone = new()
                    {
                        AreaCode = "11",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };
            string requestBody = JsonSerializer.Serialize(command);
            string? format = null;

            // Assert
            HttpResponseDeserializerException exception = Assert.Throws<HttpResponseDeserializerException>(() => HttpRequestDeserializer.Deserialize<UpdateContactRequestCommand>(requestBody, format));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.ContentType.Should().Be(format);
        }
    }
}