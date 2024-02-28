namespace DataProcessor.Database;

internal record SqliteSettings
{
    public const string SectionKey = "Sqlite";

    [Required]
    public required string ConnectionString { get; init; }
}
