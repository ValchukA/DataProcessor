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

                ProcessInstrumentStatus(instrumentStatus);

                await _bus.Publish(instrumentStatus, stoppingToken);

                _logger.LogInformation("Published statuses from {FilePath}", statusFile.Path);

                await _statusFilesRepository.DeleteAsync(statusFile);
            });
        }
    }

    private void ProcessInstrumentStatus(InstrumentStatusMessage instrumentStatus)
    {
        foreach (var deviceStatus in instrumentStatus.DeviceStatuses)
        {
            var randomIndex = Random.Shared.Next(_moduleStates.Length);
            deviceStatus.RapidControlStatus.ModuleState = _moduleStates[randomIndex];

            _logger.LogInformation(
                "Set module state of {ModuleCategoryId} to {ModuleState}",
                deviceStatus.ModuleCategoryId,
                deviceStatus.RapidControlStatus.ModuleState);
        }
    }
}
