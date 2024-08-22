using MassTransit;
using Microsoft.Extensions.Options;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Endpoints;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Integrations.RabbitMQ.Configurations;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Integrations.RabbitMQ.Adapters
{
    public class RabbitMQAdapter(ISendEndpointProvider sendEndpointProvider, IOptionsMonitor<RabbitMQConnectionConfiguration> connectionConfiguration) : IQueue
    {
        private readonly ISendEndpointProvider _sendEndpointProvider = sendEndpointProvider;
        private readonly RabbitMQConnectionConfiguration _connectionConfiguration = connectionConfiguration.CurrentValue;

        public async Task PublishMessageAsync<T>(T message, IQueueMessageHeader header, QueueEndpoint queueEndpoint)
        {
            if (message is null)
            {
                throw new Exception("");
            }
            Uri endpointAddress = _connectionConfiguration.GetEndpointAddress(queueEndpoint);
            ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(endpointAddress);
            await sendEndpoint.Send(message, context =>
            {
                context.Headers.Set("", header.CorrelationId);
                context.Headers.Set("", header.MessageId);
                context.Headers.Set("", header.MessageType);
                context.Headers.Set("", header.Source);
                context.Headers.Set("", header.Timestamp);
            });
        }
    }
}