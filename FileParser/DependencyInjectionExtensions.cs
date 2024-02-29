namespace FileParser;

internal static class DependencyInjectionExtensions
{
    public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetRequiredSection(RabbitMqSettings.SectionKey).Get<RabbitMqSettings>()!;
        Validator.ValidateObject(rabbitMqSettings, new ValidationContext(rabbitMqSettings));

        services.AddMassTransit(config => config.UsingRabbitMq((_, rabbitMqConfig) =>
            rabbitMqConfig.Host(rabbitMqSettings.Host, settings =>
            {
                settings.Username(rabbitMqSettings.Username);
                settings.Password(rabbitMqSettings.Password);
            })));
    }
}
