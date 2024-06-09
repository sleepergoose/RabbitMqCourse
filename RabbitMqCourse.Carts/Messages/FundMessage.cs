using RabbitMqCourse.Shared;

namespace RabbitMqCourse.Carts.Messages;

public record class FundMessage(long CustomerId, decimal CurrentFunds) : IMessage;
