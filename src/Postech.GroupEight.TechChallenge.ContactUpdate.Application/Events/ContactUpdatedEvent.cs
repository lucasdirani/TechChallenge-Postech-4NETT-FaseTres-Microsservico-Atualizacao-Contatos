using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events
{
    public record ContactUpdatedEvent : IApplicationEvent
    {
        public Guid ContactId { get; init; }
        public required string ContactFirstName { get; init; }
        public required string ContactLastName { get; init; }
        public required string ContactEmail { get; init; }
        public required string ContactPhoneNumber { get; init; }

        public Guid GetEventId() => ContactId;
    }
}