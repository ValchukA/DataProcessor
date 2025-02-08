namespace MessagingContracts;

public record CombinedOvenStatusMessage : CombinedStatusMessage
{
    public required bool UseTemperatureControl { get; init; }

    public required bool OvenOn { get; init; }

    public required double TemperatureActual { get; init; }

    public required double TemperatureRoom { get; init; }

    public required double MaximumTemperatureLimit { get; init; }

    public required int ValvePosition { get; init; }

    public required int ValveRotations { get; init; }

    public required bool Buzzer { get; init; }
}
