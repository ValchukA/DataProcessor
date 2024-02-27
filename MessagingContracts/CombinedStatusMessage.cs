namespace MessagingContracts;

[JsonDerivedType(typeof(CombinedOvenStatusMessage), nameof(CombinedOvenStatusMessage))]
[JsonDerivedType(typeof(CombinedPumpStatusMessage), nameof(CombinedPumpStatusMessage))]
[JsonDerivedType(typeof(CombinedSamplerStatusMessage), nameof(CombinedSamplerStatusMessage))]
public abstract record CombinedStatusMessage
{
    public required ModuleStateMessage ModuleState { get; set; }

    public required bool IsBusy { get; set; }

    public required bool IsReady { get; set; }

    public required bool IsError { get; set; }

    public required bool KeyLock { get; set; }
}
