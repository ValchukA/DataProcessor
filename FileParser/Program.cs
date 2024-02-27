var builder = Host.CreateApplicationBuilder(args);

var rabbitMqSettings = builder.Configuration
    .GetRequiredSection(RabbitMqSettings.SectionKey)
    .Get<RabbitMqSettings>()!;

Validator.ValidateObject(rabbitMqSettings, new ValidationContext(rabbitMqSettings));

builder.Services.AddMassTransit(config => config.UsingRabbitMq((_, rabbitMqConfig) =>
    rabbitMqConfig.Host(rabbitMqSettings.Host, settings =>
    {
        settings.Username(rabbitMqSettings.Username);
        settings.Password(rabbitMqSettings.Password);
    })));

builder.Services.AddHostedService<StatusesProducer>();
builder.Services.AddSingleton<IStatusFilesRepository, LocalStatusFilesRepository>();
builder.Services.AddSingleton<IStatusFileDeserializer, XmlStatusFileDeserializer>();
builder.Services.AddOptions<LocalFilesSettings>()
    .BindConfiguration(LocalFilesSettings.SectionKey)
    .ValidateDataAnnotations();

builder.Build().Run();
