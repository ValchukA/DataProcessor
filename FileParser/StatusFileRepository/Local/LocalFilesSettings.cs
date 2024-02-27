namespace FileParser.StatusFileRepository.Local;

internal record LocalFilesSettings
{
    public const string SectionKey = "LocalFiles";

    [Required]
    public required string StatusesFolderPath { get; init; }
}
