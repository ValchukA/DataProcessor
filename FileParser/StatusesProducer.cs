namespace FileParser;

internal class StatusesProducer(
    IStatusFilesRepository statusFilesRepository,
    IStatusFileDeserializer statusFileDeserializer,
    IBus bus,
    ILogger<StatusesProducer> logger)
    : BackgroundService
{
    private readonly IStatusFilesRepository _statusFilesRepository = statusFilesRepository;
    private readonly IStatusFileDeserializer _statusFileDeserializer = statusFileDeserializer;
    private readonly IBus _bus = bus;
    private readonly ILogger<StatusesProducer> _logger = logger;

    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(1));
    private readonly ModuleStateMessage[] _moduleStates = Enum.GetValues<ModuleStateMessage>();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            var statusFiles = await _statusFilesRepository.GetAllAsync();

            await Parallel.ForEachAsync(statusFiles, async (statusFile, _) =>
            {
                InstrumentStatusMessage instrumentStatus;

                try
                {
                    instrumentStatus = _statusFileDeserializer.Deserialize(statusFile.Contents);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "An exception occurred while deserializing file at {FilePath}", statusFile.Path);

                    return;
                }

                var processedInstrumentStatus = ProcessInstrumentStatus(instrumentStatus);

                await _bus.Publish(processedInstrumentStatus, stoppingToken);

                _logger.LogInformation("Published statuses from {FilePath}", statusFile.Path);

                await _statusFilesRepository.DeleteAsync(statusFile);
            });
        }
    }

    private InstrumentStatusMessage ProcessInstrumentStatus(InstrumentStatusMessage instrumentStatus)
    {
        var processedDeviceStatuses = instrumentStatus.DeviceStatuses.Select(deviceStatus =>
        {
            var randomIndex = Random.Shared.Next(_moduleStates.Length);

            var processedDeviceStatus = deviceStatus with
            {
                RapidControlStatus = deviceStatus.RapidControlStatus with { ModuleState = _moduleStates[randomIndex] },
            };

            _logger.LogInformation(
                "Set module state of {ModuleCategoryId} to {ModuleState}",
                processedDeviceStatus.ModuleCategoryId,
                processedDeviceStatus.RapidControlStatus.ModuleState);

            return processedDeviceStatus;
        });

        return instrumentStatus with { DeviceStatuses = processedDeviceStatuses.ToArray() };
    }
}
