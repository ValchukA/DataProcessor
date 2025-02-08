namespace MessagingContracts;

public record DeviceStatusMessage
{
    public required string ModuleCategoryId { get; init; }

    public required int IndexWithinRole { get; init; }

    public required CombinedStatusMessage RapidControlStatus { get; init; }
}
