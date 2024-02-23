namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IYeastRepository YeastRepository { get; }
        IBatchRepository BatchRepository { get; }
        INutrientRepository NutrientRepository { get; }
        IUserBatchRepository UserBatchRepository { get; }
        IBatchLogEntryRepository BatchLogEntryRepository { get; }
        INutrientAdditionRepository NutrientAdditionRepository { get; }
        IUserRepository UserRepository { get; }
        IUnitOfWork UnitOfWork {get;}
    }
}
