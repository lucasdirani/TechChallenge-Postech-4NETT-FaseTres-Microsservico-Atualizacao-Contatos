namespace Postech.GroupEight.TechChallenge.ContactUpdate.Core.Interfaces.Entities
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        DateTime? ModifiedAt { get; }
    }
}