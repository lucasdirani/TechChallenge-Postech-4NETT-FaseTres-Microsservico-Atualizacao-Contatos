using System.Text.Json;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Deserializers.Exceptions;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Deserializers
{
    public static class HttpRequestDeserializer
    {
        public static TRequest? Deserialize<TRequest>(string? requestBody, string? contentType)
        {
            return string.IsNullOrEmpty(requestBody) ? default : contentType switch
            {
                "application/json" => JsonSerializer.Deserialize<TRequest>(requestBody),
                "application/json; charset=utf-8" => JsonSerializer.Deserialize<TRequest>(requestBody),
                _ => throw new HttpResponseDeserializerException("The given content type is not supported", contentType)
            };
        }
    }
}