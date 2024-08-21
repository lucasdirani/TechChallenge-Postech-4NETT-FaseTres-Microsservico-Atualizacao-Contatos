namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging 
{
    public interface IQueue
    {
        Task PublishMessageAsync<T>(T message); 
    }
}