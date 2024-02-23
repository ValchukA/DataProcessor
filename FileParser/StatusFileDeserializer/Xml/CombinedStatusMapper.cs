namespace FileParser.StatusFileDeserializer.Xml;

[Mapper]
internal static partial class CombinedStatusMapper
{
    [MapDerivedType<Contracts.CombinedOvenStatus, CombinedOvenStatus>]
    [MapDerivedType<Contracts.CombinedPumpStatus, CombinedPumpStatus>]
    [MapDerivedType<Contracts.CombinedSamplerStatus, CombinedSamplerStatus>]
    public static partial CombinedStatus ToMessagingContract(Contracts.CombinedStatus combinedStatus);
}
