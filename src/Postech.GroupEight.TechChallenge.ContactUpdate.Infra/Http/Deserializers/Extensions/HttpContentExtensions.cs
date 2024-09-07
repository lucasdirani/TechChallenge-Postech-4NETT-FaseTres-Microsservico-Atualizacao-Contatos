using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Deserializers.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HttpContentExtensions
    {
        public static async Task<T?> AsAsync<T>(this HttpContent httpContent)
        {
            return await JsonSerializer.DeserializeAsync<T>(await httpContent.ReadAsStreamAsync());
        }
    }
}