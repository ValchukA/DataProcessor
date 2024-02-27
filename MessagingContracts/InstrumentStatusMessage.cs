namespace MessagingContracts;

public record InstrumentStatusMessage
{
    public required string PackageId { get; set; }

    public required DeviceStatusMessage[] DeviceStatuses { get; set; }
}
