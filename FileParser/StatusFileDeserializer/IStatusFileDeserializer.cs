namespace FileParser.StatusFileDeserializer;

internal interface IStatusFileDeserializer
{
    InstrumentStatusMessage Deserialize(string statusFileContents);
}
