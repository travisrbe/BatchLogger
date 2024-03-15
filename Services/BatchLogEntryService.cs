using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Service.Abstractions;

namespace Services
{
    internal sealed class BatchLogEntryService : IBatchLogEntryService
    {
        private readonly IRepositoryManager _repositoryManager;
        public BatchLogEntryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<BatchLogEntryDto> Create(string userId, BatchLogEntryDto batchLogEntryDto, CancellationToken cancellationToken = default)
        {
            var UserOwnsBatch = await _repositoryManager.BatchRepository
                .UserOwnsBatch(userId, batchLogEntryDto.BatchId, cancellationToken);
            var UserContributesBatch = await _repositoryManager.BatchRepository
                .UserContributesBatch(userId, batchLogEntryDto.BatchId, cancellationToken);

            batchLogEntryDto.UserId = userId; //tampering prevention

            if (UserOwnsBatch || UserContributesBatch)
            {
                BatchLogEntry batchLogEntry = batchLogEntryDto.Adapt<BatchLogEntry>();
                _repositoryManager.BatchLogEntryRepository.Create(batchLogEntry);
                await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

                //Return the Log Entry from database to reflect accurate datetimes
                Guid savedBatchLogEntryId = batchLogEntry.Id;
                batchLogEntry = await _repositoryManager.BatchLogEntryRepository
                    .FindSingleOrDefaultAsync(x => x.Id == savedBatchLogEntryId)
                    ?? throw new BatchLogEntryNotFoundException(savedBatchLogEntryId);

                return batchLogEntry.Adapt<BatchLogEntryDto>();
            }
            else
            {
                throw new UserNotAuthorizedException(batchLogEntryDto.UserId, batchLogEntryDto.BatchId);
            }
        }
        public async Task<IEnumerable<BatchLogEntryDto>> GetBatchLogEntries(string userId, Guid batchId, CancellationToken cancellationToken = default)
        {
            var UserOwnsBatch = await _repositoryManager.BatchRepository
                .UserOwnsBatch(userId, batchId, cancellationToken);
            var UserContributesBatch = await _repositoryManager.BatchRepository
                .UserContributesBatch(userId, batchId, cancellationToken);
            if (UserOwnsBatch || UserContributesBatch)
            {
                
                var batchLogEntries = await _repositoryManager.BatchLogEntryRepository
                    .FindByConditionAsync(e => e.BatchId == batchId && e.IsDeleted == false);

                var batchLogEntryDtos = batchLogEntries.Adapt<IEnumerable<BatchLogEntryDto>>();
                return batchLogEntryDtos.OrderByDescending(x => x.UpdateDate);
            }
            else
            {
                throw new UserNotAuthorizedException(userId, batchId);
            }
        }
        public async Task<BatchLogEntryDto> Update(string userId, BatchLogEntryDto batchLogEntryDto, CancellationToken cancellationToken = default)
        {
            var UserOwnsBatch = await _repositoryManager.BatchRepository
                .UserOwnsBatch(batchLogEntryDto.UserId, batchLogEntryDto.BatchId, cancellationToken);
            var dbBatchLogEntry = _repositoryManager.BatchLogEntryRepository
                .FindByCondition(x => x.Id == batchLogEntryDto.Id).SingleOrDefaultAsync().Result
                ?? throw new BatchLogEntryNotFoundException(batchLogEntryDto.BatchId);

            if (UserOwnsBatch || dbBatchLogEntry.UserId == userId)
            {
                BatchLogEntry batchLogEntry = batchLogEntryDto.Adapt<BatchLogEntry>();
                _repositoryManager.BatchLogEntryRepository.Update(batchLogEntry);
                await _repositoryManager.UnitOfWork.SaveChangesAsync();

                //Return the batch from database to reflect accurate datetimes
                Guid savedBatchLogEntryId = batchLogEntry.Id;
                batchLogEntry = await _repositoryManager.BatchLogEntryRepository
                    .FindSingleOrDefaultAsync(x => x.Id == savedBatchLogEntryId)
                    ?? throw new BatchLogEntryNotFoundException(savedBatchLogEntryId);

                return batchLogEntry.Adapt<BatchLogEntryDto>();
            }
            else
            {
                throw new UserNotAuthorizedException(userId, batchLogEntryDto.BatchId);
            }
        }

        public async Task<BatchLogEntryDto> Delete(string userId, BatchLogEntryDto batchLogEntryDto, CancellationToken cancellationToken = default)
        {
            var UserOwnsBatch = await _repositoryManager.BatchRepository
                .UserOwnsBatch(batchLogEntryDto.UserId, batchLogEntryDto.BatchId, cancellationToken);
            var dbBatchLogEntry = _repositoryManager.BatchLogEntryRepository
                .FindByCondition(x => x.Id == batchLogEntryDto.Id)
                .SingleOrDefaultAsync().Result
                ?? throw new BatchLogEntryNotFoundException(batchLogEntryDto.BatchId);

            if (UserOwnsBatch || dbBatchLogEntry.UserId == userId)
            {
                BatchLogEntry batchLogEntry = dbBatchLogEntry.Adapt<BatchLogEntry>();
                batchLogEntry.IsDeleted = true;
                _repositoryManager.BatchLogEntryRepository.Delete(batchLogEntry);
                await _repositoryManager.UnitOfWork.SaveChangesAsync();
                return batchLogEntryDto;
            }
            else
            {
                throw new UserNotAuthorizedException(userId, batchLogEntryDto.BatchId);
            }
        }
    }
}
