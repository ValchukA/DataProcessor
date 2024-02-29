var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<StatusesProducer>();
builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddSingleton<IStatusFilesRepository, LocalStatusFilesRepository>();
builder.Services.AddSingleton<IStatusFileDeserializer, XmlStatusFileDeserializer>();
builder.Services.AddOptions<LocalFilesSettings>()
    .BindConfiguration(LocalFilesSettings.SectionKey)
    .ValidateDataAnnotations();

builder.Build().Run();
