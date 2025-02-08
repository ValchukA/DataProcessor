namespace FileParser.StatusFileDeserializer.Xml.Contracts;

public abstract record CombinedStatus
{
    public required string ModuleState { get; init; }

    public required bool IsBusy { get; init; }

    public required bool IsReady { get; init; }

    public required bool IsError { get; init; }

    public required bool KeyLock { get; init; }
}
