using System.Net;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Bogus;
using FluentAssertions;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Deserializers.Extensions;
using Postech.GroupEight.TechChallenge.ContactUpdate.IntegrationTests.Configurations.Base;
using Postech.GroupEight.TechChallenge.ContactUpdate.IntegrationTests.Fixtures;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.IntegrationTests.Suite.Infra.Controllers
{
    [Collection("Integration Tests")]
    public class ContactsControllerTests(IntegrationTestFixture fixture) : IntegrationTestBase(fixture)
    {
        private readonly Faker _faker = new("pt_BR");
        
        [Fact(DisplayName = "Request to update a contact's data at the /contacts/{contactId} endpoint")]
        [Trait("Action", "/contacts/{contactId}")]
        public async Task UpdateContactEndpoint_RequestAnUpdateOfContactData_ShouldReturn202Accepted()
        {
            // Arrange
            Guid contactId = Guid.NewGuid();
            UpdateContactRequestCommand requestCommand = new()
            {
                ContactId = contactId,
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
                        AreaCode = "21",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"/contacts/{contactId}")
            {
                Content = new StringContent(JsonSerializer.Serialize(requestCommand), Encoding.UTF8, "application/json"),
            });

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Accepted);
            GenericResponseCommand<UpdateContactResponseCommand>? responseMessageContent = await responseMessage.Content.AsAsync<GenericResponseCommand<UpdateContactResponseCommand>>();
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Data.Should().NotBeNull();
            responseMessageContent?.IsSuccessfullyProcessed.Should().BeTrue();
            responseMessageContent?.Data?.ContactId.Should().Be(contactId);
            responseMessageContent?.Data?.ContactNotifiedForUpdateAt.Should().BeOnOrBefore(DateTime.UtcNow);
            responseMessageContent?.Data?.IsContactNotifiedForUpdate.Should().BeTrue();
            responseMessageContent?.Messages.Should().BeNull();
        }

        [Fact(DisplayName = "Request body not provided for /contacts/{contactId} endpoint")]
        [Trait("Action", "/contacts/{contactId}")]
        public async Task UpdateContactEndpoint_RequestBodyNotProvided_ShouldReturn400BadRequest()
        {
            // Arrange
            Guid contactId = Guid.NewGuid();

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"/contacts/{contactId}"));

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            GenericResponseCommand<UpdateContactResponseCommand>? responseMessageContent = await responseMessage.Content.AsAsync<GenericResponseCommand<UpdateContactResponseCommand>>();
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Messages.Should().NotBeNullOrEmpty();
        }

        [Fact(DisplayName = "Content-Type not supported for /contacts/{contactId} endpoint")]
        [Trait("Action", "/contacts/{contactId}")]
        public async Task UpdateContactEndpoint_ContentTypeNotSupported_ShouldReturn415UnsupportedMediaType()
        {
            // Arrange
            Guid contactId = Guid.NewGuid();
            UpdateContactRequestCommand requestCommand = new()
            {
                ContactId = contactId,
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
                        AreaCode = "21",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };
            XmlSerializer serializer = new(typeof(UpdateContactRequestCommand));
            using StringWriter stringWriter = new();
            serializer.Serialize(stringWriter, requestCommand);
            
            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"/contacts/{contactId}")
            {
                Content = new StringContent(stringWriter.ToString(), Encoding.UTF8, "application/xml"),
            });

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
        }

        [Theory(DisplayName = "First name currently registered for contact provided incorrectly at the /contacts/{contactId} endpoint")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("MaximilianoGustavianoLeonardocristovãoBeneditinoalberto")]
        [InlineData(null)]
        [Trait("Action", "/contacts/{contactId}")]
        public async Task UpdateContactEndpoint_FirstNameCurrentlyRegisteredForContactProvidedIncorrectly_ShouldReturn400BadRequest(string currentContactFirstName)
        {
            // Arrange
            Guid contactId = Guid.NewGuid();
            UpdateContactRequestCommand requestCommand = new()
            {
                ContactId = contactId,
                CurrentContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = currentContactFirstName,
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
                        AreaCode = "21",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"/contacts/{contactId}")
            {
                Content = new StringContent(JsonSerializer.Serialize(requestCommand), Encoding.UTF8, "application/json"),
            });

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            GenericResponseCommand<UpdateContactResponseCommand>? responseMessageContent = await responseMessage.Content.AsAsync<GenericResponseCommand<UpdateContactResponseCommand>>();
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Messages.Should().NotBeNullOrEmpty();
        }

        [Theory(DisplayName = "Last name currently registered for contact provided incorrectly at the /contacts/{contactId} endpoint")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("De Oliveira da Silva Monteiro dos Santos Pereira de Albuquerque e Souza")]
        [InlineData(null)]
        [Trait("Action", "/contacts/{contactId}")]
        public async Task UpdateContactEndpoint_LastNameCurrentlyRegisteredForContactProvidedIncorrectly_ShouldReturn400BadRequest(string currentContactLastName)
        {
            // Arrange
            Guid contactId = Guid.NewGuid();
            UpdateContactRequestCommand requestCommand = new()
            {
                ContactId = contactId,
                CurrentContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = currentContactLastName
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
                        AreaCode = "21",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"/contacts/{contactId}")
            {
                Content = new StringContent(JsonSerializer.Serialize(requestCommand), Encoding.UTF8, "application/json"),
            });

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            GenericResponseCommand<UpdateContactResponseCommand>? responseMessageContent = await responseMessage.Content.AsAsync<GenericResponseCommand<UpdateContactResponseCommand>>();
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Messages.Should().NotBeNullOrEmpty();
        }

        [Theory(DisplayName = "Email address currently registered for contact provided incorrectly at the /contacts/{contactId} endpoint")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("nome.sobrenome.extremamente.longo.que.ultrapassa.sessenta.caracteres@email.com")]
        [InlineData(null)]
        [Trait("Action", "/contacts/{contactId}")]
        public async Task UpdateContactEndpoint_EmailAddressCurrentlyRegisteredForContactProvidedIncorrectly_ShouldReturn400BadRequest(string currentContactEmailAddress)
        {
            // Arrange
            Guid contactId = Guid.NewGuid();
            UpdateContactRequestCommand requestCommand = new()
            {
                ContactId = contactId,
                CurrentContactData = new()
                {
                    ContactName = new()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = _faker.Name.LastName()
                    },
                    ContactEmail = new()
                    {
                        Address = currentContactEmailAddress
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
                        AreaCode = "21",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"/contacts/{contactId}")
            {
                Content = new StringContent(JsonSerializer.Serialize(requestCommand), Encoding.UTF8, "application/json"),
            });

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            GenericResponseCommand<UpdateContactResponseCommand>? responseMessageContent = await responseMessage.Content.AsAsync<GenericResponseCommand<UpdateContactResponseCommand>>();
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Messages.Should().NotBeNullOrEmpty();
        }

        [Theory(DisplayName = "Phone number currently registered for contact provided incorrectly at the /contacts/{contactId} endpoint")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("9768542")]
        [InlineData("9768542836")]
        [InlineData(null)]
        [Trait("Action", "/contacts/{contactId}")]
        public async Task UpdateContactEndpoint_PhoneNumberCurrentlyRegisteredForContactProvidedIncorrectly_ShouldReturn400BadRequest(string currentContactPhoneNumber)
        {
            // Arrange
            Guid contactId = Guid.NewGuid();
            UpdateContactRequestCommand requestCommand = new()
            {
                ContactId = contactId,
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
                        Number = currentContactPhoneNumber
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
                        AreaCode = "21",
                        Number = _faker.Phone.PhoneNumber("9########")
                    }
                }
            };

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"/contacts/{contactId}")
            {
                Content = new StringContent(JsonSerializer.Serialize(requestCommand), Encoding.UTF8, "application/json"),
            });

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            GenericResponseCommand<UpdateContactResponseCommand>? responseMessageContent = await responseMessage.Content.AsAsync<GenericResponseCommand<UpdateContactResponseCommand>>();
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Messages.Should().NotBeNullOrEmpty();
        }
    }
}