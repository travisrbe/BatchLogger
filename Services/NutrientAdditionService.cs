using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Service.Abstractions;

namespace Services
{
    internal sealed class NutrientAdditionService : INutrientAdditionService
    {
        private readonly IRepositoryManager _repositoryManager;
        public NutrientAdditionService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<NutrientAdditionDto> Create(string userId, NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken)
        {
            bool UserOwnsBatch = await _repositoryManager.BatchRepository
                .UserOwnsBatch(userId, nutrientAdditionDto.BatchId, cancellationToken);
            if (UserOwnsBatch)
            {
                NutrientAddition nutrientAddition = nutrientAdditionDto.Adapt<NutrientAddition>();
                _repositoryManager.NutrientAdditionRepository.Create(nutrientAddition);
                await _repositoryManager.UnitOfWork.SaveChangesAsync();
                nutrientAdditionDto = nutrientAddition.Adapt<NutrientAdditionDto>();
                return nutrientAdditionDto;
            }
            else
            {
                throw new UserDoesNotOwnBatchException(userId, nutrientAdditionDto.BatchId);
            }
        }
        public async Task<IEnumerable<NutrientAdditionDto>> GetByBatchId(string userId, Guid batchId, CancellationToken cancellationToken)
        {
            bool UserOwnsBatch = await _repositoryManager.BatchRepository
                .UserOwnsBatch(userId, batchId, cancellationToken);
            bool UserContributesBatch = await _repositoryManager.BatchRepository
                .UserContributesBatch(userId, batchId, cancellationToken);
            if (UserOwnsBatch || UserContributesBatch)
            {
                var nutrientAdditions = await _repositoryManager.NutrientAdditionRepository.GetByBatchIdAsync(batchId, cancellationToken).ConfigureAwait(false);
                return nutrientAdditions.Adapt<IEnumerable<NutrientAdditionDto>>();
            }
            else
            {
                throw new UserNotAuthorizedException(userId, batchId);
            }
        }

        public async Task<NutrientAdditionDto> Update(string userId, NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken)
        {
            bool UserOwnsBatch = await _repositoryManager.BatchRepository
                .UserOwnsBatch(userId, nutrientAdditionDto.BatchId, cancellationToken);
            var nutrientAddition = await _repositoryManager.NutrientAdditionRepository
                .FindSingleOrDefaultAsync(x => x.Id == nutrientAdditionDto.Id) ?? throw new NutrientNotFoundException(nutrientAdditionDto.Id);

            if (UserOwnsBatch)
            {
                _repositoryManager.NutrientAdditionRepository
                    .Update(nutrientAdditionDto.Adapt<NutrientAddition>());
                await _repositoryManager.UnitOfWork.SaveChangesAsync();
                nutrientAdditionDto = nutrientAddition.Adapt<NutrientAdditionDto>();
                return nutrientAdditionDto;
            }
            else
            {
                throw new UserDoesNotOwnBatchException(userId, nutrientAdditionDto.BatchId);
            }
        }
        public async Task<NutrientAdditionDto> Delete(string userId, NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken)
        {
            bool UserOwnsBatch = await _repositoryManager.BatchRepository
                .UserOwnsBatch(userId, nutrientAdditionDto.BatchId, cancellationToken);
            if (UserOwnsBatch)
            {
                NutrientAddition nutrientAddition = nutrientAdditionDto.Adapt<NutrientAddition>();
                nutrientAddition.IsDeleted = true;
                _repositoryManager.NutrientAdditionRepository
                    .Delete(nutrientAddition);
                await _repositoryManager.UnitOfWork.SaveChangesAsync();
                return nutrientAdditionDto;
            }
            else
            {
                throw new UserDoesNotOwnBatchException(userId, nutrientAdditionDto.BatchId);
            }
        }
    }
}
