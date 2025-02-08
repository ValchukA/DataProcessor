namespace FileParser.StatusFileDeserializer.Xml.Contracts;

public record CombinedOvenStatus : CombinedStatus
{
    public required bool UseTemperatureControl { get; init; }

    public required bool OvenOn { get; init; }

    [XmlElement("Temperature_Actual")]
    public required double TemperatureActual { get; init; }

    [XmlElement("Temperature_Room")]
    public required double TemperatureRoom { get; init; }

    public required double MaximumTemperatureLimit { get; init; }

    [XmlElement("Valve_Position")]
    public required int ValvePosition { get; init; }

    [XmlElement("Valve_Rotations")]
    public required int ValveRotations { get; init; }

    public required bool Buzzer { get; init; }
}
