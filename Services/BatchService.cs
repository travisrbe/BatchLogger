﻿using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    internal class BatchService : IBatchService
    {
        private readonly IRepositoryManager _repositoryManager;
        public BatchService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<BatchDto?>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var batches = await _repositoryManager.BatchRepository.GetAllAsync(cancellationToken);
            var batchesDto = batches.Adapt<IEnumerable<BatchDto>>();
            return batchesDto;
        }

        public async Task<BatchDto> GetByIdAsync(string userId, Guid id, CancellationToken cancellationToken = default)
        {
            var batch = await _repositoryManager.BatchRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new BatchNotFoundException(id);

            if (batch.OwnerUserId != userId)
            {
                throw new UserDoesNotOwnBatchException(userId, batch.Id);
            }
            var batchDto = batch.Adapt<BatchDto>();
            return batchDto;
        }
        public async Task<IEnumerable<BatchDto?>> GetByUserIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var batches = await _repositoryManager.BatchRepository.GetUserBatchesAsync(id, cancellationToken);
            var batchesDto = batches.Adapt<IEnumerable<BatchDto>>();
            return batchesDto;
        }

        public async Task<IEnumerable<BatchDto?>> GetByOwnedAsync(string id, CancellationToken cancellationToken = default)
        {
            var batches = await _repositoryManager.BatchRepository.GetOwnedBatchesAsync(id, cancellationToken);
            var batchesDto = batches.Adapt<IEnumerable<BatchDto>>();
            return batchesDto;
        }
        public async Task<BatchDto> Update(string userId, BatchDto batchDto, CancellationToken cancellationToken = default)
        {
            //get the batch, check if null
            var DbBatchDto = await _repositoryManager.BatchRepository.GetByIdAsync(batchDto.Id, cancellationToken) 
                ?? throw new BatchNotFoundException(batchDto.Id);
            //check if user really owns it
            if (DbBatchDto.OwnerUserId != userId)
            {
                throw new UserDoesNotOwnBatchException(userId, DbBatchDto.Id);
            }

            //update
            Batch batch = batchDto.Adapt<Batch>();
            _repositoryManager.BatchRepository.Update(batch);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            return batchDto;
            
        }

        public async Task<BatchDto> Create(BatchDto batchDto, CancellationToken cancellationToken = default)
        {
            Batch batch = batchDto.Adapt<Batch>();
            _repositoryManager.BatchRepository.Create(batch);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            //EF hydrates this with auto-generated values like Id.
            return batch.Adapt<BatchDto>();
        }

        public async Task<BatchDto> Delete(string userId, BatchDto batchDto, CancellationToken cancellationToken = default)
        {
            //get the batch, check if null
            var DbBatchDto = await _repositoryManager.BatchRepository.GetByIdAsync(batchDto.Id, cancellationToken)
                ?? throw new BatchNotFoundException(batchDto.Id);
            //check if user really owns it
            if (DbBatchDto.OwnerUserId != userId)
            {
                throw new UserDoesNotOwnBatchException(userId, DbBatchDto.Id);
            }

            //update
            Batch batch = batchDto.Adapt<Batch>();
            batch.IsDeleted = true;
            _repositoryManager.BatchRepository.Delete(batch);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            return batchDto;
        }
    }
}
