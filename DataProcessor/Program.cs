var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddSqlite(builder.Configuration);

var host = builder.Build();

if (builder.Configuration.GetValue<bool>("ApplyMigrations"))
{
    var dbContext = host.Services.GetRequiredService<DataProcessorDbContext>();
    dbContext.Database.Migrate();
}

host.Run();
