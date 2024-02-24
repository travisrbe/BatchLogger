using Contracts;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Service.Abstractions;

namespace Services
{
    public sealed class YeastService : IYeastService
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

        //Add-back during admin sprint
        //public async Task Delete(Guid yeastId, CancellationToken cancellationToken = default)
        //{
        //    var yeast = await _repositoryManager.YeastRepository.GetByIdAsync(yeastId, cancellationToken);
        //    if (yeast == null)
        //    {
        //        throw new YeastNotFoundException(yeastId);
        //    }
        //    else
        //    {
        //        _repositoryManager.YeastRepository.Delete(yeast);
        //        await _repositoryManager.UnitOfWork.SaveChangesAsync();
        //    }
        //}
    }
}
