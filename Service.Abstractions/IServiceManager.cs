namespace Service.Abstractions
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IYeastService YeastService { get; }
        IBatchService BatchService { get; }
        INutrientService NutrientService { get; }
        IUserBatchService UserBatchService { get; }
        IBatchLogEntryService BatchLogEntryService { get; }
        INutrientAdditionService NutrientAdditionService { get; }
        IStackPresetService StackPresetService { get; }
    }
}
