namespace FileParser;

internal record RabbitMqSettings
{
    public const string SectionKey = "RabbitMq";

    [Required]
    public required string Host { get; init; }

    [Required]
    public required string Username { get; init; }

    [Required]
    public required string Password { get; init; }
}
