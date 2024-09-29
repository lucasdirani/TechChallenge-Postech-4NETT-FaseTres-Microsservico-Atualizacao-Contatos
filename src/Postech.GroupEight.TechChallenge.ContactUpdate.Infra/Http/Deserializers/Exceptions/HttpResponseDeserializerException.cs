using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Deserializers.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class HttpResponseDeserializerException(string? message, string? contentType) : Exception(message)
    {
        public string? ContentType { get; } = contentType;
    }
}