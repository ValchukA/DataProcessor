namespace DataProcessor.Database.Entities;

internal record DeviceStatusEntity
{
    [Key]
    [MaxLength(50)]
    public required string ModuleCategoryId { get; set; }

    public required ModuleStateEntity ModuleState { get; set; }
}
