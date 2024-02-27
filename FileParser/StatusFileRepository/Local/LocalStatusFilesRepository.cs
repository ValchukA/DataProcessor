namespace FileParser.StatusFileRepository.Local;

internal class LocalStatusFilesRepository(IOptions<LocalFilesSettings> options) : IStatusFilesRepository
{
    private readonly string _folderPath = options.Value.StatusesFolderPath;

    public async IAsyncEnumerable<StatusFile> GetAllAsync()
    {
        foreach (var path in Directory.EnumerateFiles(_folderPath))
        {
            yield return new StatusFile
            {
                Path = path,
                Contents = await File.ReadAllTextAsync(path),
            };
        }
    }

    public Task DeleteAsync(StatusFile statusFile)
    {
        File.Delete(statusFile.Path);

        return Task.CompletedTask;
    }
}
