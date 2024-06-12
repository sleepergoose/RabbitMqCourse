namespace RabbitMqCourse.Shared;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(string exchange, string routingKey, TMessage message,
        Dictionary<string, object>? headers = null) where TMessage : class, IMessage;
}
