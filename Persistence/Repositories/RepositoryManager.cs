using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IYeastRepository> _lazyYeastRepository;
        private readonly Lazy<IBatchRepository> _lazyBatchRepository;
        private readonly Lazy<INutrientRepository> _lazyNutrientRepository;
        private readonly Lazy<IUserBatchRepository> _lazyUserBatchRepository;
        private readonly Lazy<IBatchLogEntryRepository> _lazyBatchLogEntryRepository;
        private readonly Lazy<INutrientAdditionRepository> _lazyNutrientAdditionRepository;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoryManager(DataContext context)
        {
            _lazyYeastRepository = new Lazy<IYeastRepository>(() => new YeastRepository(context));
            _lazyBatchRepository = new Lazy<IBatchRepository>(() => new BatchRepository(context));
            _lazyNutrientRepository = new Lazy<INutrientRepository>(() => new NutrientRepository(context));
            _lazyUserBatchRepository = new Lazy<IUserBatchRepository>(() => new UserBatchRepository(context));
            _lazyBatchLogEntryRepository = new Lazy<IBatchLogEntryRepository>(() => new BatchLogEntryRepository(context));
            _lazyNutrientAdditionRepository = new Lazy<INutrientAdditionRepository>(() => new NutrientAdditionRepository(context));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(context));
        }

        public IYeastRepository YeastRepository => _lazyYeastRepository.Value;
        public IBatchRepository BatchRepository => _lazyBatchRepository.Value;
        public INutrientRepository NutrientRepository => _lazyNutrientRepository.Value;
        public IUserBatchRepository UserBatchRepository => _lazyUserBatchRepository.Value;
        public IBatchLogEntryRepository BatchLogEntryRepository => _lazyBatchLogEntryRepository.Value;
        public INutrientAdditionRepository NutrientAdditionRepository => _lazyNutrientAdditionRepository.Value;
        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    }
}
