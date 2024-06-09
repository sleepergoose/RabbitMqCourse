
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMqCourse.Shared.Publishers;

internal class MessagePublisher : IMessagePublisher
{
    private readonly IModel _channel;
    public MessagePublisher(IChannelFactory _factory)
        => _channel = _factory.Create();

    public Task PublishAsync<TMessage>(string exchange, string routingKey, TMessage message) where TMessage : class, IMessage
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = _channel.CreateBasicProperties();

        properties.Headers = new Dictionary<string, object>()
        {
            { "MyHeaderKey", "MyHeaderValue" }
        };

        _channel.ExchangeDeclare(exchange, "topic", false, false);

        _channel.BasicPublish(exchange, routingKey, properties, body);

        return Task.CompletedTask;
    }
}
