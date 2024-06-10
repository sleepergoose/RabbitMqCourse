using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace RabbitMqCourse.Shared.Subscribers;

internal sealed class MessageSubscriber : IMessageSubscriber
{
    private readonly IModel _channel;

    public MessageSubscriber(IChannelFactory _factory)
        => _channel = _factory.Create();

    public IMessageSubscriber SubscribeMessage<TMessage>(string queue, string routingKey, string exchange,
        Func<TMessage, BasicDeliverEventArgs, Task> handle) where TMessage : class, IMessage
    {
        _channel.ExchangeDeclare(exchange: exchange, type: "topic", durable: false, autoDelete: false);
        _channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false);
        _channel.QueueBind(queue, exchange, routingKey);

        // Specifies how many messages will be consumed at once
        // _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, eventArgs) =>
        {
            // How to get headers if are needed
            // var headers = eventArgs.BasicProperties.Headers;

            var body = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var message = JsonSerializer.Deserialize<TMessage>(body);

            if (message is not null)
            {
                await handle(message, eventArgs);
            }

            // Non-acknoledgement (in case of an error, for example):
            //_channel.BasicNack(deliveryTag: eventArgs.DeliveryTag, multiple: false, requeue: false);

            _channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
        };

        _channel.BasicConsume(queue, autoAck: true, consumer);

        return this;
    }
}
