namespace FileParser.StatusFileRepository;

internal interface IStatusFilesRepository
{
    Task<IReadOnlyCollection<StatusFile>> GetAllAsync();

    Task DeleteAsync(StatusFile statusFile);
}
