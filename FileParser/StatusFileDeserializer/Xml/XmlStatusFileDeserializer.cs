namespace FileParser.StatusFileDeserializer.Xml;

internal class XmlStatusFileDeserializer : IStatusFileDeserializer
{
    private const string _errorPrefix = "XML error:";

    private readonly XmlSerializer _instrumentSerializer = new(typeof(InstrumentStatus));
    private readonly XmlSerializer[] _rapidControlSerializers =
    [
        new(typeof(CombinedOvenStatus)),
        new(typeof(CombinedPumpStatus)),
        new(typeof(CombinedSamplerStatus))
    ];

    public InstrumentStatusMessage Deserialize(string statusFileContents)
    {
        var fileContentsReader = new StringReader(statusFileContents);
        var instrument = _instrumentSerializer.Deserialize(fileContentsReader) as InstrumentStatus
            ?? throw new ArgumentException($"{_errorPrefix} {nameof(InstrumentStatus)} is null");

        return new InstrumentStatusMessage
        {
            PackageId = instrument.PackageId,
            DeviceStatuses = instrument.DeviceStatuses?.Select(ToDeviceStatus).ToArray() ?? [],
        };
    }

    private DeviceStatusMessage ToDeviceStatus(DeviceStatus device)
    {
        var rapidControlXml = device.RapidControlStatus
            ?? throw new ArgumentException($"{_errorPrefix} {nameof(DeviceStatus.RapidControlStatus)} is null");

        var rapidControlReader = XmlReader.Create(new StringReader(rapidControlXml));

        var rapidControlSerializer = _rapidControlSerializers
            .FirstOrDefault(serializer => serializer.CanDeserialize(rapidControlReader))
            ?? throw new ArgumentException($"{_errorPrefix} Not supported type");

        var rapidControl = rapidControlSerializer.Deserialize(rapidControlReader) as CombinedStatus
            ?? throw new ArgumentException($"{_errorPrefix} {nameof(CombinedStatus)} is null");

        return new DeviceStatusMessage
        {
            ModuleCategoryId = device.ModuleCategoryId,
            IndexWithinRole = device.IndexWithinRole,
            RapidControlStatus = CombinedStatusMapper.ToMessage(rapidControl),
        };
    }
}
