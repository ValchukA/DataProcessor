namespace FileParser.StatusFileDeserializer.Xml.Contracts;

public abstract record CombinedStatus
{
    public required string ModuleState { get; set; }

    public required bool IsBusy { get; set; }

    public required bool IsReady { get; set; }

    public required bool IsError { get; set; }

    public required bool KeyLock { get; set; }
}
