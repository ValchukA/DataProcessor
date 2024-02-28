namespace FileParser.StatusFileDeserializer.Xml;

[Mapper]
internal static partial class CombinedStatusMapper
{
    [MapDerivedType<CombinedOvenStatus, CombinedOvenStatusMessage>]
    [MapDerivedType<CombinedPumpStatus, CombinedPumpStatusMessage>]
    [MapDerivedType<CombinedSamplerStatus, CombinedSamplerStatusMessage>]
    public static partial CombinedStatusMessage ToMessage(CombinedStatus combinedStatus);
}
