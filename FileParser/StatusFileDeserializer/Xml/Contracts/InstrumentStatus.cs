namespace FileParser.StatusFileDeserializer.Xml.Contracts;

public record InstrumentStatus
{
    [XmlElement("PackageID")]
    public required string PackageId { get; init; }

    [XmlElement("DeviceStatus")]
    public required DeviceStatus[]? DeviceStatuses { get; init; }
}
