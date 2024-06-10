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
        _channel.ExchangeDeclare(exchange, "topic", false, false);
        _channel.QueueDeclare(queue, false, false, false);
        _channel.QueueBind(queue, exchange, routingKey);

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
        };

        _channel.BasicConsume(queue, true, consumer);

        return this;
    }
}
