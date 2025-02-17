namespace FileParser.StatusFileDeserializer;

internal interface IStatusFileDeserializer
{
    public InstrumentStatusMessage Deserialize(string statusFileContents);
}
