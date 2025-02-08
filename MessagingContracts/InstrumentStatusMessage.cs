namespace MessagingContracts;

public record InstrumentStatusMessage
{
    public required string PackageId { get; init; }

    public required IReadOnlyCollection<DeviceStatusMessage> DeviceStatuses { get; init; }
}
