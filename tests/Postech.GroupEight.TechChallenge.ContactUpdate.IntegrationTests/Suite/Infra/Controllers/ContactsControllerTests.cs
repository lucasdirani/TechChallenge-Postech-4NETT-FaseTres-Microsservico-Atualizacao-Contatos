using System.Net;
using System.Text;
using System.Text.Json;
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
    }
}