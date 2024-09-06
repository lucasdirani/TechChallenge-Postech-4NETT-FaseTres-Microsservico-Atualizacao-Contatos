using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Postech.GroupEight.TechChallenge.ContactUpdate.IntegrationTests.Configurations.Factories;
using Postech.GroupEight.TechChallenge.ContactUpdate.IntegrationTests.Fixtures;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.IntegrationTests.Configurations.Base
{
    [ExcludeFromCodeCoverage]
    public class IntegrationTestBase : IClassFixture<IntegrationTestFixture>
    {
        protected readonly HttpClient HttpClient;
        protected readonly ContactUpdateAppWebApplicationFactory WebApplicationFactory;

        protected IntegrationTestBase(IntegrationTestFixture fixture)
        {
            WebApplicationFactory = fixture.WebApplicationFactory;
            HttpClient = WebApplicationFactory.CreateClient();
        }

        protected T GetService<T>() 
            where T : notnull
        {
            IServiceScope scope = WebApplicationFactory.Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<T>();
        }
    }
}