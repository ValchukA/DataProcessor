namespace FileParser.StatusFileRepository.Local;

internal class LocalStatusFilesRepository(IOptions<LocalFilesSettings> options) : IStatusFilesRepository
{
    private readonly string _folderPath = options.Value.StatusesFolderPath;

    public async Task<IReadOnlyCollection<StatusFile>> GetAllAsync()
    {
        var statusFiles = new List<StatusFile>();

        foreach (var path in Directory.EnumerateFiles(_folderPath))
        {
            statusFiles.Add(new StatusFile
            {
                Path = path,
                Contents = await File.ReadAllTextAsync(path),
            });
        }

        return statusFiles;
    }

    public Task DeleteAsync(StatusFile statusFile)
    {
        File.Delete(statusFile.Path);

        return Task.CompletedTask;
    }
}
