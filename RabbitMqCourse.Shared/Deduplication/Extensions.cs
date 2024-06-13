using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMqCourse.Shared.Deduplication;

public static class Extensions
{
    public static IMessagingConfiguration AddDeduplication<TContext>(this IMessagingConfiguration configure)
        where TContext : DbContext
    {
        configure.Services.TryDecorate(typeof(IMessageHandler<>), typeof(DeduplicationMessageHandlerDecorator<>));
        configure.Services.AddScoped<Func<DbContext>>(sp => sp.GetRequiredService<TContext>);

        return configure;
    }
}
