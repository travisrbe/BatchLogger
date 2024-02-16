using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IYeastRepository> _lazyYeastRepository;
        private readonly Lazy<IBatchRepository> _lazyBatchRepository;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoryManager(DataContext context)
        {
            _lazyYeastRepository = new Lazy<IYeastRepository>(() => new YeastRepository(context));
            _lazyBatchRepository = new Lazy<IBatchRepository>(() => new BatchRepository(context));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(context));
        }

        public IYeastRepository YeastRepository => _lazyYeastRepository.Value;
        public IBatchRepository BatchRepository => _lazyBatchRepository.Value;
        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value; 
    }
}
