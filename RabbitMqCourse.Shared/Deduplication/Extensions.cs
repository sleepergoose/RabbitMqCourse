using EasyCronJob.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMqCourse.Shared.Deduplication;

public static class Extensions
{
    public static IMessagingConfiguration AddDeduplication<TContext>(this IMessagingConfiguration msgConfiguration,
        IConfiguration configuration) where TContext : DbContext
    {
        var options = configuration.GetOptions<DeduplicationOptions>("Deduplication");

        if (options.Enabled)
        {
            msgConfiguration.Services.AddSingleton(options);
            msgConfiguration.Services.TryDecorate(typeof(IMessageHandler<>), typeof(DeduplicationMessageHandlerDecorator<>));
            msgConfiguration.Services.AddScoped<Func<DbContext>>(sp => sp.GetRequiredService<TContext>);

            msgConfiguration.Services.ApplyResulation<DeduplicationCronJob>(opts =>
            {
                opts.CronExpression = options.Interval;
                opts.TimeZoneInfo = TimeZoneInfo.Local;
                opts.CronFormat = Cronos.CronFormat.Standard;
            });
        }

        return msgConfiguration;
    }
}
