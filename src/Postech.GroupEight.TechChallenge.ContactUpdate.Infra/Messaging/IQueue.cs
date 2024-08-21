using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging 
{
    public interface IQueue
    {
        Task PublishMessageAsync<T>(T message, IQueueMessageHeader header); 
    }
}