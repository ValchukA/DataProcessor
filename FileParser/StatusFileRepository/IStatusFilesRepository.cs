namespace FileParser.StatusFileRepository;

internal interface IStatusFilesRepository
{
    public Task<IReadOnlyCollection<StatusFile>> GetAllAsync();

    public Task DeleteAsync(StatusFile statusFile);
}
