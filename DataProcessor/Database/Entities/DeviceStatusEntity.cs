namespace DataProcessor.Database.Entities;

internal record DeviceStatusEntity
{
    [Key]
    [MaxLength(50)]
    public required string ModuleCategoryId { get; init; }

    public required ModuleStateEntity ModuleState { get; init; }
}
