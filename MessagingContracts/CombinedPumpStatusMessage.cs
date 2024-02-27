namespace MessagingContracts;

public record CombinedPumpStatusMessage : CombinedStatusMessage
{
    public required string Mode { get; set; }

    public required int Flow { get; set; }

    public required int PercentB { get; set; }

    public required int PercentC { get; set; }

    public required int PercentD { get; set; }

    public required double MinimumPressureLimit { get; set; }

    public required double MaximumPressureLimit { get; set; }

    public required double Pressure { get; set; }

    public required bool PumpOn { get; set; }

    public required int Channel { get; set; }
}
