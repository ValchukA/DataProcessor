namespace MessagingContracts;

public record CombinedPumpStatusMessage : CombinedStatusMessage
{
    public required string Mode { get; init; }

    public required int Flow { get; init; }

    public required int PercentB { get; init; }

    public required int PercentC { get; init; }

    public required int PercentD { get; init; }

    public required double MinimumPressureLimit { get; init; }

    public required double MaximumPressureLimit { get; init; }

    public required double Pressure { get; init; }

    public required bool PumpOn { get; init; }

    public required int Channel { get; init; }
}
