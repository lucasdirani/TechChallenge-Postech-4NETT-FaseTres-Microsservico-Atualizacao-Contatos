using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Enumerators;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Models
{
    [ExcludeFromCodeCoverage]
    public record Notification
    {
        public required string Message { get; init; }
        public NotificationType Type { get; init; }
    }
}