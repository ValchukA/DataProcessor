var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<StatusesProducer>();
builder.Services.AddSingleton<IStatusFilesRepository, LocalStatusFilesRepository>();
builder.Services.AddSingleton<IStatusFileDeserializer, XmlStatusFileDeserializer>();
builder.Services.AddOptions<LocalFilesSettings>()
    .BindConfiguration(LocalFilesSettings.SectionKey)
    .ValidateDataAnnotations();

var rabbitMqSettings = builder.Configuration
    .GetRequiredSection(RabbitMqSettings.SectionKey)
    .Get<RabbitMqSettings>()!;

Validator.ValidateObject(rabbitMqSettings, new ValidationContext(rabbitMqSettings));
builder.Services.AddMassTransit(config => config.UsingRabbitMq((context, rabbitMqConfig)
    => rabbitMqConfig.Host(rabbitMqSettings.Host, settings =>
    {
        settings.Username(rabbitMqSettings.Username);
        settings.Password(rabbitMqSettings.Password);
    })));

builder.Build().Run();
