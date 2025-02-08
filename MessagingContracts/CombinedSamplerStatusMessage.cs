namespace MessagingContracts;

public record CombinedSamplerStatusMessage : CombinedStatusMessage
{
    public required int Status { get; init; }

    public required string Vial { get; init; }

    public required int Volume { get; init; }

    public required int MaximumInjectionVolume { get; init; }

    public required string RackL { get; init; }

    public required string RackR { get; init; }

    public required int RackInf { get; init; }

    public required bool Buzzer { get; init; }
}
