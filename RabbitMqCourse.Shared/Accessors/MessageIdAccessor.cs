namespace RabbitMqCourse.Shared.Accessors;

internal sealed class MessageIdAccessor : IMessageIdAccessor
{
    private readonly AsyncLocal<string> _vallue = new();

    public string GetMessageId() => _vallue.Value;

    public void SetMessageId(string messageId) => _vallue.Value = messageId;
}
