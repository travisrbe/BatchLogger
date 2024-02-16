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
        private readonly Lazy<IYeastService> _lazyYeastService;
        private readonly Lazy<IBatchService> _lazyBatchService;
        private readonly Lazy<IUserService> _lazyUserService;

        public ServiceManager(IRepositoryManager repositoryManager, IHttpContextAccessor httpContextAccessor)
        {
            _lazyYeastService = new Lazy<IYeastService>(() => new YeastService(repositoryManager));
            _lazyBatchService = new Lazy<IBatchService>(() => new BatchService(repositoryManager));
            _lazyUserService= new Lazy<IUserService>(() => new UserService(httpContextAccessor));
        }

        public IYeastService YeastService => _lazyYeastService.Value;
        public IBatchService BatchService => _lazyBatchService.Value;
        public IUserService UserService => _lazyUserService.Value;

    }
}
