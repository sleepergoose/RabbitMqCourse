
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMqCourse.Shared.Dispatchers;

internal sealed class MessageDispatcher : IMessageDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public MessageDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    async Task IMessageDispatcher.DispatchAsync<TMessage>(TMessage message)
    {
        var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IMessageHandler<TMessage>>();
        await handler.HandleAsync(message);
    }
}
