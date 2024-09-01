using System.Diagnostics.CodeAnalysis;
using MassTransit;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers.Constants;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Integrations.RabbitMQ.Adapters
{
    [ExcludeFromCodeCoverage]
    public class RabbitMQAdapter(IPublishEndpoint publishEndpoint) : IQueue
    {
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task PublishMessageAsync<T>(T message, IQueueMessageHeader header) 
            where T : notnull
        {
            await _publishEndpoint.Publish(message, context =>
            {
                context.Headers.Set(QueueMessageHeaderName.CorrelationId, header.CorrelationId);
                context.Headers.Set(QueueMessageHeaderName.MessageId, header.MessageId);
                context.Headers.Set(QueueMessageHeaderName.MessageType, header.MessageType);
                context.Headers.Set(QueueMessageHeaderName.Source, header.Source);
                context.Headers.Set(QueueMessageHeaderName.Timestamp, header.Timestamp);
                context.CorrelationId = header.CorrelationId;
                context.MessageId = header.MessageId;
            });
        }
    }
}