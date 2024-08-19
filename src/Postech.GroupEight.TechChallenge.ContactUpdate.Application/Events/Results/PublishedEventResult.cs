namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results 
{
    public record PublishedEventResult
    {
        public Guid EventId { get; init; }
        public DateTime PublishedAt { get; init; }
    }
}