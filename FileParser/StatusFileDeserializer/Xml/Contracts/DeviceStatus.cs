namespace FileParser.StatusFileDeserializer.Xml.Contracts;

public record DeviceStatus
{
    [XmlElement("ModuleCategoryID")]
    public required string ModuleCategoryId { get; set; }

    public required int IndexWithinRole { get; set; }

    public required string RapidControlStatus { get; set; }
}
