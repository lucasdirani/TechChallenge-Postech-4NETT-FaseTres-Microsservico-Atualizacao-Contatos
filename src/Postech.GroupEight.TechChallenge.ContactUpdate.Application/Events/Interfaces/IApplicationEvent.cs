namespace Postech.GroupEight.TechChallenge.ContactManagement.Events.Interfaces
{
    public interface IApplicationEvent
    {
        string EventType { get; }
        Guid GetEventId();
    }
}