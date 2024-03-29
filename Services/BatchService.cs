﻿using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Service.Abstractions;
using Services.ServiceHelpers;

namespace Services
{
    internal class BatchService : IBatchService
    {
        private readonly IRepositoryManager _repositoryManager;
        private CalculatorHelper calcHelper = new CalculatorHelper();
        public BatchService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<BatchDto> Create(BatchDto batchDto, CancellationToken cancellationToken = default)
        {
            Batch batch = batchDto.Adapt<Batch>();
            batch.YeastId = _repositoryManager.YeastRepository.GetAllAsync(cancellationToken).Result[0].Id;
            _repositoryManager.BatchRepository.Create(batch);
            //hydrates Brix, Sugar PPM, and Total Target Yan
            //calcHelper.CalculateTargetYan(ref batch);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            //Return the batch from database to reflect accurate datetimes
            Guid savedBatchId = batch.Id;
            batch = await _repositoryManager.BatchRepository.GetByIdAsync(savedBatchId, cancellationToken) 
                ?? throw new BatchNotFoundException(savedBatchId);

            //EF hydrates this with auto-generated values like Id.
            return batch.Adapt<BatchDto>();
        }

        public async Task<IEnumerable<BatchDto?>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var batches = await _repositoryManager.BatchRepository.GetAllAsync(cancellationToken);
            var batchesDto = batches.Adapt<IEnumerable<BatchDto>>();
            return batchesDto.OrderByDescending(b => b.CreateDate);
        }

        public async Task<BatchDto> ShareByIdAsync(string userId, Guid id, CancellationToken cancellationToken = default)
        {
            Batch batch = await _repositoryManager.BatchRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new BatchNotFoundException(id);

            if (!batch.IsPublic)
            {
                throw new BatchNotFoundException(id);
            }
            else return batch.Adapt<BatchDto>();
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

        public async Task<IEnumerable<BatchDto?>> GetByUserIdAsync(string id,
            CancellationToken cancellationToken = default)
        {
            var batches = await _repositoryManager.BatchRepository.GetUserBatchesAsync(id, cancellationToken);
            var batchesDto = batches.Adapt<IEnumerable<BatchDto>>();
            return batchesDto.OrderByDescending(b => b.CreateDate);
        }

        public async Task<IEnumerable<BatchDto?>> GetByOwnedAsync(string id, CancellationToken cancellationToken = default)
        {
            var batches = await _repositoryManager.BatchRepository.GetOwnedBatchesAsync(id, cancellationToken);
            var batchesDto = batches.Adapt<IEnumerable<BatchDto>>();
            return batchesDto.OrderByDescending(b => b.CreateDate);
        }

        public async Task<BatchDto> Update(string userId, BatchDto batchDto, CancellationToken cancellationToken = default)
        {
            //get the batch, check if null
            var dbBatch = await _repositoryManager.BatchRepository.GetByIdAsync(batchDto.Id, cancellationToken) 
                ?? throw new BatchNotFoundException(batchDto.Id);
            //check if user really owns it
            if (dbBatch.OwnerUserId != userId)
            {
                throw new UserDoesNotOwnBatchException(userId, dbBatch.Id);
            }
            Batch batch = batchDto.Adapt<Batch>();
            Yeast yeast = await _repositoryManager.YeastRepository.GetByIdAsync(batch.YeastId, cancellationToken)
                ?? throw new YeastNotFoundException(batch.YeastId);
            batch.Yeast = yeast;
            calcHelper.CalculateTargetYan(ref batch);
            _repositoryManager.BatchRepository.Update(batch);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            //Return the batch from database to reflect accurate datetimes
            Guid savedBatchId = batch.Id;
            batch = await _repositoryManager.BatchRepository.GetByIdAsync(savedBatchId, cancellationToken)
                ?? throw new BatchNotFoundException(savedBatchId);

            return batch.Adapt<BatchDto>();
        }
        public async Task<BatchDto> Delete(string userId, BatchDto batchDto, CancellationToken cancellationToken = default)
        {
            //get the batch, check if null
            var DbBatch = await _repositoryManager.BatchRepository.GetByIdAsync(batchDto.Id, cancellationToken)
                ?? throw new BatchNotFoundException(batchDto.Id);
            //check if user really owns it
            if (DbBatch.OwnerUserId != userId)
            {
                throw new UserDoesNotOwnBatchException(userId, DbBatch.Id);
            }

            //update
            foreach (var na in DbBatch.NutrientAdditions)
            {
                na.IsDeleted = true;
#pragma warning disable CS0618 // Hard Delete Approved
                _repositoryManager.NutrientAdditionRepository.HardDelete(na);

            }
            foreach (var le in DbBatch.LogEntries)
            {
                le.IsDeleted = true;
                _repositoryManager.BatchLogEntryRepository.HardDelete(le);
            }
            foreach (var ub in DbBatch.UserBatches)
            {
                ub.IsDeleted = true;
                _repositoryManager.UserBatchRepository.HardDelete(ub);
            }

            DbBatch.IsDeleted = true;
            _repositoryManager.BatchRepository.HardDelete(DbBatch);
#pragma warning restore CS0618 // Hard Delete Approved
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            return batchDto;
        }
    }
}
