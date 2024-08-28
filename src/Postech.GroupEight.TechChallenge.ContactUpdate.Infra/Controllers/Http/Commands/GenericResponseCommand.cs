using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Models;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands
{
    [ExcludeFromCodeCoverage]
    public record GenericResponseCommand<T>
    {
        [JsonPropertyName("data")]
        public T? Data { get; init; }

        [JsonPropertyName("messages")]
        public IEnumerable<Notification>? Messages { get; init; }

        [JsonPropertyName("isSuccessfullyProcessed")]
        public bool IsSuccessfullyProcessed => Data is not null;
    }
}