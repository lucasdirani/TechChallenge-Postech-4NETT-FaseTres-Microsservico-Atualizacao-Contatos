using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Requests.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HttpRequestExtensions
    {
        public static IDictionary<string, object?> GetRouteValues(this HttpRequest httpRequest)
        {
            return httpRequest.RouteValues.ToDictionary(route => route.Key, route => route.Value);
        }
    }
}