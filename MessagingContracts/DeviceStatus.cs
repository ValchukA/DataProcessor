namespace MessagingContracts;

public record DeviceStatus
{
    public required string ModuleCategoryId { get; set; }

    public required int IndexWithinRole { get; set; }

    public required CombinedStatus RapidControlStatus { get; set; }
}
