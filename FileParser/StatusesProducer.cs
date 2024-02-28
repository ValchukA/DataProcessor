namespace FileParser;

internal class StatusesProducer(IStatusFilesRepository statusFilesRepository, IStatusFileDeserializer statusFileDeserializer, IBus bus)
    : BackgroundService
{
    private readonly IStatusFilesRepository _statusFilesRepository = statusFilesRepository;
    private readonly IStatusFileDeserializer _statusFileDeserializer = statusFileDeserializer;
    private readonly IBus _bus = bus;

    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(1));
    private readonly ModuleStateMessage[] _moduleStates = Enum.GetValues<ModuleStateMessage>();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            var statusFiles = _statusFilesRepository.GetAllAsync();

            await Parallel.ForEachAsync(statusFiles, async (statusFile, _) =>
            {
                var instrumentStatus = _statusFileDeserializer.Deserialize(statusFile.Contents);
                SetRandomModuleStates(instrumentStatus.DeviceStatuses);

                await _bus.Publish(instrumentStatus, stoppingToken);
                await _statusFilesRepository.DeleteAsync(statusFile);
            });
        }
    }

    private void SetRandomModuleStates(IEnumerable<DeviceStatusMessage> deviceStatuses)
    {
        foreach (var deviceStatus in deviceStatuses)
        {
            var randomIndex = Random.Shared.Next(_moduleStates.Length);
            deviceStatus.RapidControlStatus.ModuleState = _moduleStates[randomIndex];
        }
    }
}
