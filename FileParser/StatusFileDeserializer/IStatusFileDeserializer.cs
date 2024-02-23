namespace FileParser.StatusFileDeserializer;

internal interface IStatusFileDeserializer
{
    InstrumentStatus Deserialize(string statusFileContents);
}
