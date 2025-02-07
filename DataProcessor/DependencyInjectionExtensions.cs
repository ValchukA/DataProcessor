namespace DataProcessor;

internal static class DependencyInjectionExtensions
{
    public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetRequiredSection(RabbitMqSettings.SectionKey).Get<RabbitMqSettings>()!;
        Validator.ValidateObject(rabbitMqSettings, new ValidationContext(rabbitMqSettings));

        services.AddMassTransit(config =>
        {
            config.AddConsumer<StatusesConsumer>();
            config.UsingRabbitMq((context, rabbitMqConfig) =>
            {
                rabbitMqConfig.ConfigureEndpoints(context);
                rabbitMqConfig.Host(rabbitMqSettings.Host, settings =>
                {
                    settings.Username(rabbitMqSettings.Username);
                    settings.Password(rabbitMqSettings.Password);
                });
            });
        });
    }

    public static void AddSqlite(this IServiceCollection services, IConfiguration configuration)
    {
        var sqliteSettings = configuration.GetRequiredSection(SqliteSettings.SectionKey).Get<SqliteSettings>()!;
        Validator.ValidateObject(sqliteSettings, new ValidationContext(sqliteSettings));

        services.AddDbContext<DataProcessorDbContext>(options => options.UseSqlite(sqliteSettings.ConnectionString));
    }
}
