namespace MessagingContracts;

public record CombinedSamplerStatusMessage : CombinedStatusMessage
{
    public required int Status { get; set; }

    public required string Vial { get; set; }

    public required int Volume { get; set; }

    public required int MaximumInjectionVolume { get; set; }

    public required string RackL { get; set; }

    public required string RackR { get; set; }

    public required int RackInf { get; set; }

    public required bool Buzzer { get; set; }
}
