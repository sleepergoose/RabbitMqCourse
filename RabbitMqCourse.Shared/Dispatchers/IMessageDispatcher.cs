namespace RabbitMqCourse.Shared.Dispatchers;

internal interface IMessageDispatcher
{
    Task DispatchAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
}
