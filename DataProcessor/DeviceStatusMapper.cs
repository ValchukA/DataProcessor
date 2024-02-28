namespace DataProcessor;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
internal static partial class DeviceStatusMapper
{
    [MapProperty("RapidControlStatus.ModuleState", "ModuleState")]
    public static partial DeviceStatusEntity ToEntity(DeviceStatusMessage deviceStatusMessage);
}
