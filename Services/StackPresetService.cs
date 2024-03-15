using Contracts;
using Domain.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public sealed class StackPresetService : IStackPresetService
    {
        private readonly IRepositoryManager _repositoryManager;
        public StackPresetService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<StackPresetDto>> GetOrderedStackPresets(CancellationToken cancellationToken)
        {
            var stackPresets = await _repositoryManager.StackPresetRepository.FindAllAsync();
            return stackPresets.Adapt<IEnumerable<StackPresetDto>>().OrderBy(sp => sp.Order);
        }
    }
}