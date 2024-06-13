
using RabbitMqCourse.Carts.Messages;
using RabbitMqCourse.Shared;
using RabbitMqCourse.Shared.Dispatchers;

namespace RabbitMqCourse.Carts.Services;

internal sealed class MessagingBackgroundService : BackgroundService
{
    private readonly IMessageSubscriber _messageSubscriber;
    private readonly ILogger<MessagingBackgroundService> _logger;
    private readonly IMessageDispatcher _dispatcher;

    public MessagingBackgroundService(ILogger<MessagingBackgroundService> logger, IMessageSubscriber messageSubscriber,
        IMessageDispatcher dispatcher)
    {
        _logger = logger;
        _messageSubscriber = messageSubscriber;
        _dispatcher = dispatcher;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageSubscriber
            .SubscribeMessage<FundMessage>("carts-service-funds-message-words", "Country.#", "Funds",
            async (message, args) =>
            {
                _logger.LogInformation($"Received message for customer: {message.CustomerId}, Funds: {message.CurrentFunds}, RoutingKey: {args.RoutingKey}");
                await _dispatcher.DispatchAsync(message);
            });

        return Task.CompletedTask;
    }
}
