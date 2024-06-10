using RabbitMqCourse.Shared;

namespace RabbitMqCourse.Funds.Messages;

public record class FundMessage(long CustomerId, decimal CurrentFunds) : IMessage;
