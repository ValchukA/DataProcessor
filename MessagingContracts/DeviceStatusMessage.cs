namespace MessagingContracts;

public record DeviceStatusMessage
{
    public required string ModuleCategoryId { get; set; }

    public required int IndexWithinRole { get; set; }

    public required CombinedStatusMessage RapidControlStatus { get; set; }
}
