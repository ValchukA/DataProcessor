namespace MessagingContracts;

public record InstrumentStatus
{
    public required string PackageId { get; set; }

    public required DeviceStatus[] DeviceStatuses { get; set; }
}
