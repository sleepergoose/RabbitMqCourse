
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMqCourse.Shared.Publishers;

internal sealed class MessagePublisher : IMessagePublisher
{
    private readonly IModel _channel;
    public MessagePublisher(IChannelFactory _factory)
        => _channel = _factory.Create();

    public async Task PublishAsync<TMessage>(string exchange, string routingKey, TMessage message,
        string messageId = default, Dictionary<string, object>? headers = null) where TMessage : class, IMessage
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = _channel.CreateBasicProperties();

        properties.Headers = headers;
        properties.MessageId = messageId ?? Guid.NewGuid().ToString("N");

        // Allows you to create a Task and control its completion manually.
        // It's particularly useful when you're dealing with asynchronous operations that don't naturally produce tasks,
        // such as events or callback-based APIs.
        // Create an instance of TaskCompletionSource<T> with the desired result type T
        var tcs = new TaskCompletionSource();

        // Turn on confirmation on the publisher side
        _channel.ConfirmSelect();

        // An ack event subscription and a handler. 
        // Ack means that the message was sent to the exchange (if the message is nor mandatory nor persistent [mandatory: false])
        _channel.BasicAcks += (sender, args) =>
        {
            // This fires second to show that the message was delivered to exchange.

            // manually complete the task by calling methods like SetResult, SetException, or SetCanceled
            // on the TaskCompletionSource instance.
            tcs.SetResult();
        };

        
        // An ack event subscription and a handler. 
        // This fires if no route.
        _channel.BasicReturn += (sender, args) =>
        {
            // This fires first to show that there is no routing.
        };

        _channel.ExchangeDeclare(exchange, type: "topic", durable: false, autoDelete: false);

        _channel.BasicPublish(exchange, routingKey, mandatory: true, properties, body);


        // The Task property of the TaskCompletionSource instance returns a Task<T> object,
        // which you can await or use like any other task.
        await tcs.Task;
    }
}
