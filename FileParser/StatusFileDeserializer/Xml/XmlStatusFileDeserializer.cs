namespace FileParser.StatusFileDeserializer.Xml;

internal class XmlStatusFileDeserializer : IStatusFileDeserializer
{
    private const string _errorPrefix = "XML error:";

    private readonly XmlSerializer _instrumentSerializer = new(typeof(Contracts.InstrumentStatus));
    private readonly XmlSerializer[] _rapidControlSerializers =
    [
        new(typeof(Contracts.CombinedOvenStatus)),
        new(typeof(Contracts.CombinedPumpStatus)),
        new(typeof(Contracts.CombinedSamplerStatus))
    ];

    public InstrumentStatus Deserialize(string statusFileContents)
    {
        var fileContentsReader = new StringReader(statusFileContents);
        var instrument = _instrumentSerializer.Deserialize(fileContentsReader) as Contracts.InstrumentStatus
            ?? throw new ArgumentException($"{_errorPrefix} {nameof(Contracts.InstrumentStatus)} is null");

        return new InstrumentStatus
        {
            PackageId = instrument.PackageId,
            DeviceStatuses = instrument.DeviceStatuses?.Select(ToDeviceStatus).ToArray() ?? [],
        };
    }

    private DeviceStatus ToDeviceStatus(Contracts.DeviceStatus device)
    {
        var rapidControlXml = device.RapidControlStatus
            ?? throw new ArgumentException($"{_errorPrefix} {nameof(DeviceStatus.RapidControlStatus)} is null");

        var rapidControlReader = XmlReader.Create(new StringReader(rapidControlXml));

        var rapidControlSerializer = _rapidControlSerializers
            .FirstOrDefault(serializer => serializer.CanDeserialize(rapidControlReader))
            ?? throw new ArgumentException($"{_errorPrefix} Not supported type");

        var rapidControl = rapidControlSerializer.Deserialize(rapidControlReader) as Contracts.CombinedStatus
            ?? throw new ArgumentException($"{_errorPrefix} {nameof(Contracts.CombinedStatus)} is null");

        return new DeviceStatus
        {
            ModuleCategoryId = device.ModuleCategoryId,
            IndexWithinRole = device.IndexWithinRole,
            RapidControlStatus = CombinedStatusMapper.ToMessagingContract(rapidControl),
        };
    }
}
