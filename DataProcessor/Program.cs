var builder = Host.CreateApplicationBuilder(args);

var rabbitMqSettings = builder.Configuration
    .GetRequiredSection(RabbitMqSettings.SectionKey)
    .Get<RabbitMqSettings>()!;
var sqliteSettings = builder.Configuration
    .GetRequiredSection(SqliteSettings.SectionKey)
    .Get<SqliteSettings>()!;

Validator.ValidateObject(rabbitMqSettings, new ValidationContext(rabbitMqSettings));
Validator.ValidateObject(sqliteSettings, new ValidationContext(sqliteSettings));

builder.Services.AddMassTransit(config =>
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

builder.Services.AddDbContext<DataProcessorDbContext>(options =>
    options.UseSqlite(sqliteSettings.ConnectionString));

var host = builder.Build();

if (builder.Configuration.GetValue<bool>("ApplyMigrations"))
{
    var dbContext = host.Services.GetRequiredService<DataProcessorDbContext>();
    dbContext.Database.Migrate();
}

host.Run();
