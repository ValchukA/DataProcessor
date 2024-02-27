namespace MessagingContracts;

public abstract record CombinedStatus
{
    public required ModuleState ModuleState { get; set; }

    public required bool IsBusy { get; set; }

    public required bool IsReady { get; set; }

    public required bool IsError { get; set; }

    public required bool KeyLock { get; set; }
}
