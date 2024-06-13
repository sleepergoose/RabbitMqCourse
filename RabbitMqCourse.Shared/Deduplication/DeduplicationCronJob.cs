﻿using Cronos;
using EasyCronJob.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RabbitMqCourse.Shared.Deduplication;

internal sealed class DeduplicationCronJob : CronJobService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly DeduplicationOptions _options;

    public DeduplicationCronJob(ICronConfiguration<DeduplicationCronJob> cronConfiguration, IServiceProvider serviceProvider,
        DeduplicationOptions options) 
             : base(cronConfiguration.CronExpression, cronConfiguration.TimeZoneInfo, cronConfiguration.CronFormat)
    {
        _serviceProvider = serviceProvider;
        _options = options;
    }

    public override async Task DoWork(CancellationToken cancellationToken)
    {
        await using var scope =  _serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<Func<DbContext>>()();

        var entitiesToRemove = await context.Set<DeduplicationModel>()
            .Where(p => p.ProcessedAt.AddDays(_options.MessageEvictionWindowInDays) < DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        context.Set<DeduplicationModel>().RemoveRange(entitiesToRemove);
        await context.SaveChangesAsync(cancellationToken);
    }
}
