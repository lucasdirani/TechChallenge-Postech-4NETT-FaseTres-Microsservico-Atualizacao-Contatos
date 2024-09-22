using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Postech.GroupEight.TechChallenge.ContactManagement.Events.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactManagement.Events
{
    [ExcludeFromCodeCoverage]
    public record ContactUpdatedEvent : IApplicationEvent
    {
        public Guid ContactId { get; init; }
        public required string ContactFirstName { get; init; }
        public required string ContactLastName { get; init; }
        public required string ContactEmail { get; init; }
        public required string ContactPhoneNumber { get; init; }
        public required string ContactPhoneNumberAreaCode { get; init; }
        
        [JsonIgnore]
        public string EventType { get; init; } = nameof(ContactUpdatedEvent);

        public Guid GetEventId() => ContactId;
    }
}