using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<IYeastService> _lazyYeastService;
        private readonly Lazy<IBatchService> _lazyBatchService;
        private readonly Lazy<IUserBatchService> _lazyUserBatchService;
        private readonly Lazy<IBatchLogEntryService> _lazyBatchLogEntryService;
        private readonly Lazy<INutrientAdditionService> _lazyNutrientAdditionService;
        private readonly Lazy<INutrientService> _lazyNutrientService;

        public ServiceManager(IRepositoryManager repositoryManager, IHttpContextAccessor httpContextAccessor)
        {
            _lazyUserService = new Lazy<IUserService>(() => new UserService(httpContextAccessor));
            _lazyYeastService = new Lazy<IYeastService>(() => new YeastService(repositoryManager));
            _lazyBatchService = new Lazy<IBatchService>(() => new BatchService(repositoryManager));
            _lazyUserBatchService = new Lazy<IUserBatchService>(() => new UserBatchService(repositoryManager));
            _lazyBatchLogEntryService = new Lazy<IBatchLogEntryService>(() => new BatchLogEntryService(repositoryManager));
            _lazyNutrientAdditionService = new Lazy<INutrientAdditionService> (() =>  new NutrientAdditionService(repositoryManager));
            _lazyNutrientService = new Lazy<INutrientService>(() => new NutrientService(repositoryManager));
        }

        public IUserService UserService => _lazyUserService.Value;
        public IYeastService YeastService => _lazyYeastService.Value;
        public INutrientService NutrientService => _lazyNutrientService.Value;
        public IBatchService BatchService => _lazyBatchService.Value;
        public IUserBatchService UserBatchService => _lazyUserBatchService.Value;
        public IBatchLogEntryService BatchLogEntryService => _lazyBatchLogEntryService.Value;
        public INutrientAdditionService NutrientAdditionService => _lazyNutrientAdditionService.Value;
    }
}
