
using Microsoft.EntityFrameworkCore;
using RabbitMqCourse.Shared.Accessors;

namespace RabbitMqCourse.Shared.Deduplication;

internal sealed class DeduplicationMessageHandlerDecorator<TMessage> : IMessageHandler<TMessage> where TMessage : class, IMessage
{
    private readonly IMessageHandler<TMessage> _handler;
    private readonly DbContext _context;
    private readonly IMessageIdAccessor _messageIdAccessor;

    public DeduplicationMessageHandlerDecorator(IMessageHandler<TMessage> handler, Func<DbContext> getContext,
        IMessageIdAccessor messageIdAccessor)
    {
        _handler = handler;
        _context = getContext();
        _messageIdAccessor = messageIdAccessor;
    }

    public async Task HandleAsync(TMessage message)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var messageId = _messageIdAccessor.GetMessageId();

            if (await _context.Set<DeduplicationModel>().AnyAsync(p => p.MessageId == messageId))
            {
                return;
            }

            await _handler.HandleAsync(message);

            var deduplicationModel = new DeduplicationModel
            {
                MessageId = messageId,
                ProcessedAt = DateTime.UtcNow,
            };

            await _context.Set<DeduplicationModel>().AddAsync(deduplicationModel);

            await transaction.CommitAsync();

            await _context.SaveChangesAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }


        await _handler.HandleAsync(message);
    }
}
