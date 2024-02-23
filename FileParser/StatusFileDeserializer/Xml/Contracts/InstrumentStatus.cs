namespace FileParser.StatusFileDeserializer.Xml.Contracts;

public record InstrumentStatus
{
    [XmlElement("PackageID")]
    public required string PackageId { get; set; }

    [XmlElement("DeviceStatus")]
    public required DeviceStatus[]? DeviceStatuses { get; set; }
}
