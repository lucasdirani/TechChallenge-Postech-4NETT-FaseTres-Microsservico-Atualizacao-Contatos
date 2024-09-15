using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Enumerators;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Models
{
    [ExcludeFromCodeCoverage]
    public record Notification
    {
        [JsonPropertyName("message")]
        public required string Message { get; init; }

        [JsonPropertyName("type")]
        public NotificationType Type { get; init; }
    }
}