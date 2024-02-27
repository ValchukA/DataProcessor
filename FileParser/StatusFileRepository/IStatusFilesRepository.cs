namespace FileParser.StatusFileRepository;

internal interface IStatusFilesRepository
{
    IAsyncEnumerable<StatusFile> GetAllAsync();

    Task DeleteAsync(StatusFile statusFile);
}
