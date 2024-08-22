using System.Diagnostics.CodeAnalysis;
using MassTransit;
using Microsoft.Extensions.Options;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Endpoints;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers.Constants;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Headers.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Integrations.RabbitMQ.Configurations;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Integrations.RabbitMQ.Adapters
{
    [ExcludeFromCodeCoverage]
    public class RabbitMQAdapter(ISendEndpointProvider sendEndpointProvider, IOptionsMonitor<RabbitMQConnectionConfiguration> connectionConfiguration) : IQueue
    {
        private readonly ISendEndpointProvider _sendEndpointProvider = sendEndpointProvider;
        private readonly RabbitMQConnectionConfiguration _connectionConfiguration = connectionConfiguration.CurrentValue;

        public async Task PublishMessageAsync<T>(T message, IQueueMessageHeader header, QueueEndpoint queueEndpoint) 
            where T : notnull
        {
            Uri endpointAddress = _connectionConfiguration.GetEndpointAddress(queueEndpoint);
            ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(endpointAddress);
            await sendEndpoint.Send(message, context =>
            {
                context.Headers.Set(QueueMessageHeaderName.CorrelationId, header.CorrelationId);
                context.Headers.Set(QueueMessageHeaderName.MessageId, header.MessageId);
                context.Headers.Set(QueueMessageHeaderName.MessageType, header.MessageType);
                context.Headers.Set(QueueMessageHeaderName.Source, header.Source);
                context.Headers.Set(QueueMessageHeaderName.Timestamp, header.Timestamp);
            });
        }
    }
}