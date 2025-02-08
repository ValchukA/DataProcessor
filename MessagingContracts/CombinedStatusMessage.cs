namespace MessagingContracts;

[JsonDerivedType(typeof(CombinedOvenStatusMessage), nameof(CombinedOvenStatusMessage))]
[JsonDerivedType(typeof(CombinedPumpStatusMessage), nameof(CombinedPumpStatusMessage))]
[JsonDerivedType(typeof(CombinedSamplerStatusMessage), nameof(CombinedSamplerStatusMessage))]
public abstract record CombinedStatusMessage
{
    public required ModuleStateMessage ModuleState { get; init; }

    public required bool IsBusy { get; init; }

    public required bool IsReady { get; init; }

    public required bool IsError { get; init; }

    public required bool KeyLock { get; init; }
}
