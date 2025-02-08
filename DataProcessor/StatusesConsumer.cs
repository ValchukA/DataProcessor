namespace DataProcessor;

internal class StatusesConsumer(DataProcessorDbContext dbContext, ILogger<StatusesConsumer> logger)
    : IConsumer<InstrumentStatusMessage>
{
    private readonly DataProcessorDbContext _dbContext = dbContext;
    private readonly ILogger<StatusesConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<InstrumentStatusMessage> context)
    {
        var statuses = context.Message.DeviceStatuses;
        var categoryIds = statuses.Select(status => status.ModuleCategoryId).ToList();
        var existingCategoryIds = await _dbContext.DeviceStatuses
            .Where(status => categoryIds.Contains(status.ModuleCategoryId))
            .Select(status => status.ModuleCategoryId)
            .ToListAsync();

        foreach (var status in statuses)
        {
            var statusEntity = DeviceStatusMapper.ToEntity(status);

            if (existingCategoryIds.Contains(statusEntity.ModuleCategoryId))
            {
                _dbContext.Update(statusEntity);
            }
            else
            {
                _dbContext.Add(statusEntity);
            }
        }

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Processed {DeviceStatusCount} device status(es)", statuses.Count);
    }
}
