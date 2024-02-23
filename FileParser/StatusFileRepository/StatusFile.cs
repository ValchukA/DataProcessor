namespace FileParser.StatusFileRepository;

internal record StatusFile
{
    public required string Path { get; init; }

    public required string Contents { get; init; }
}
