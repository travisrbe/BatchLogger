using Contracts;
using Domain.Repositories;
using Service.Abstractions;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Services
{
    internal sealed class YeastService : IYeastService
    {
        private readonly IRepositoryManager _repositoryManager;
        public YeastService(IRepositoryManager repositoryManager) 
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<YeastDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var yeasts = await _repositoryManager.YeastRepository.GetAllAsync(cancellationToken);
            var yeastsDto = yeasts.Adapt<IEnumerable<YeastDto>>();

            return yeastsDto;
        }

        public async Task<YeastDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var yeast = await _repositoryManager.YeastRepository.GetByIdAsync(id, cancellationToken);
            if (yeast == null)
            {
                throw new YeastNotFoundException(id);
            }
            var yeastDto = yeast.Adapt<YeastDto>();
            return yeastDto;
        }
    }
}
