namespace DataProcessor.Database;

internal class DataProcessorDbContext(DbContextOptions<DataProcessorDbContext> options) : DbContext(options)
{
    public DbSet<DeviceStatusEntity> DeviceStatuses { get; private set; }
}
