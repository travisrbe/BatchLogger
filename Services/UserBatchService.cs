﻿using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Service.Abstractions;

namespace Services
{
    internal sealed class UserBatchService : IUserBatchService
    {
        private readonly IRepositoryManager _repositoryManager;
        public UserBatchService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<UserBatchDto> Create(string userId, Guid batchId, CancellationToken cancellationToken = default)
        {
            //query db for the batch
            Batch? batch = await _repositoryManager.BatchRepository.GetByIdAsync(batchId, cancellationToken)
                ?? throw new BatchNotFoundException(batchId);
            //check user owns batch
            if (batch.OwnerUserId != userId)
            {
                throw new UserDoesNotOwnBatchException(userId, batch.Id);
            }
            //check for duplication
            var dbUserBatch = _repositoryManager.UserBatchRepository
                .FindByCondition(x => x.UserId == userId && x.BatchId == batchId) 
                ?? throw new UserBatchAlreadyExistsException(userId, batchId);

            //create and return
            UserBatch userBatch = new UserBatch()
            {
                UserId = userId,
                BatchId = batchId
            };
            _repositoryManager.UserBatchRepository.Create(userBatch);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return userBatch.Adapt<UserBatchDto>();
        }

        public async Task<UserBatchDto> Delete(string userId, UserBatchDto userBatchDto, CancellationToken cancellationToken = default)
        {
            //query db for the batch
            Batch? batch = await _repositoryManager.BatchRepository.GetByIdAsync(userBatchDto.BatchId, cancellationToken)
                ?? throw new BatchNotFoundException(userBatchDto.BatchId);
            //check user owns batch
            if (batch.OwnerUserId != userId)
            {
                throw new UserDoesNotOwnBatchException(userId, batch.Id);
            }

            //Delete and return
            UserBatch userBatch = userBatchDto.Adapt<UserBatch>();
            userBatch.IsDeleted = true;
            _repositoryManager.UserBatchRepository.Delete(userBatch);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return userBatchDto;
        }
    }
}