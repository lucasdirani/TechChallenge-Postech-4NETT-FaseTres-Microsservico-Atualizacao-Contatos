using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Endpoints;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging 
{
    public interface IQueue
    {
        Task PublishMessageAsync<T>(T message, IQueueMessageHeader header, QueueEndpoint queueEndpoint) where T : notnull; 
    }
}