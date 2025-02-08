namespace FileParser.StatusFileDeserializer.Xml.Contracts;

public record DeviceStatus
{
    [XmlElement("ModuleCategoryID")]
    public required string ModuleCategoryId { get; init; }

    public required int IndexWithinRole { get; init; }

    public required string RapidControlStatus { get; init; }
}
