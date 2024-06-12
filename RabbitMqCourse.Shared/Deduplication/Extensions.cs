using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMqCourse.Shared.Deduplication;

public static class Extensions
{
    public static IServiceCollection AddDeduplication<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.TryDecorate(typeof(IMessageHandler<>), typeof(DeduplicationMessageHandlerDecorator<>));
        services.AddScoped<Func<DbContext>>(sp => sp.GetRequiredService<TContext>);

        return services;
    }
}
