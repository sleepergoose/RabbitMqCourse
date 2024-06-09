
using RabbitMqCourse.Carts.Messages;
using RabbitMqCourse.Shared;

namespace RabbitMqCourse.Carts.Services;

internal sealed class MessagingBackgroundService : BackgroundService
{
    private readonly IMessageSubscriber _messageSubscriber;
    private readonly ILogger<MessagingBackgroundService> _logger;

    public MessagingBackgroundService(ILogger<MessagingBackgroundService> logger, IMessageSubscriber messageSubscriber)
    {
        _logger = logger;
        _messageSubscriber = messageSubscriber;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageSubscriber
            .SubscribeMessage<FundMessage>("carts-service-funds-message", "FundsMessage", "Funds",
            (message, args) =>
            {
                _logger.LogInformation($"Received message for customer: {message.CustomerId}, Funds: {message.CurrentFunds}, RoutingKey: {args.RoutingKey}");
                return Task.CompletedTask;
            });

        return Task.CompletedTask;
    }
}
