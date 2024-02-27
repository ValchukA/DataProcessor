namespace FileParser.StatusFileDeserializer.Xml.Contracts;

public record CombinedOvenStatus : CombinedStatus
{
    public required bool UseTemperatureControl { get; set; }

    public required bool OvenOn { get; set; }

    [XmlElement("Temperature_Actual")]
    public required double TemperatureActual { get; set; }

    [XmlElement("Temperature_Room")]
    public required double TemperatureRoom { get; set; }

    public required double MaximumTemperatureLimit { get; set; }

    [XmlElement("Valve_Position")]
    public required int ValvePosition { get; set; }

    [XmlElement("Valve_Rotations")]
    public required int ValveRotations { get; set; }

    public required bool Buzzer { get; set; }
}
