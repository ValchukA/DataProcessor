namespace MessagingContracts;

public record CombinedOvenStatus : CombinedStatus
{
    public required bool UseTemperatureControl { get; set; }

    public required bool OvenOn { get; set; }

    public required double TemperatureActual { get; set; }

    public required double TemperatureRoom { get; set; }

    public required double MaximumTemperatureLimit { get; set; }

    public required int ValvePosition { get; set; }

    public required int ValveRotations { get; set; }

    public required bool Buzzer { get; set; }
}
