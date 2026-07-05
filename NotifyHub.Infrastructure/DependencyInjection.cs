using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotifyHub.Application.Interfaces;
using NotifyHub.Application.Options;
using NotifyHub.Infrastructure.HostedServices;
using NotifyHub.Infrastructure.NotificationSenders;
using StackExchange.Redis;

namespace NotifyHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // SignalR
        services.AddSignalR();

        // Hangfire
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

        services.AddHangfireServer();

        // Redis
        var redisConn = configuration.GetConnectionString("Redis");
        if (!string.IsNullOrEmpty(redisConn))
        {
            if (!string.IsNullOrEmpty(redisConn))
            {
                try
                {
                    var options = ConfigurationOptions.Parse(redisConn);
                    options.AbortOnConnectFail = false;
                    var multiplexer = ConnectionMultiplexer.Connect(options);
                    services.AddSingleton<IConnectionMultiplexer>(multiplexer);
                }
                catch
                {
                    // Redis unavailable; skip registration for design-time.
                }
            }
        }
        // Options
        services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));
        services.Configure<SmsOptions>(configuration.GetSection("SmsOptions"));
        services.Configure<FcmOptions>(configuration.GetSection("FcmOptions"));

        // Notification senders
        services.AddTransient<INotificationSender, EmailNotificationSender>();
        services.AddTransient<INotificationSender, SmsNotificationSender>();
        services.AddTransient<INotificationSender, FcmNotificationSender>();
        services.AddHttpClient<FcmNotificationSender>();

        // Hosted service for outbox processing
        services.AddHostedService<NotificationDispatcherService>();

        return services;
    }
}
