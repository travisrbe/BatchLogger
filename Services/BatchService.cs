using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<IEnumerable<BatchDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var batches = await _repositoryManager.BatchRepository.GetAllAsync(cancellationToken);
            var batchesDto = batches.Adapt<IEnumerable<BatchDto>>();
            return batchesDto;
        }

        public async Task<BatchDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var batch = await _repositoryManager.BatchRepository.GetByIdAsync(id, cancellationToken);
            if (batch == null)
            {
                throw new BatchNotFoundException(id);
            }
            var batchDto = batch.Adapt<BatchDto>();
            return batchDto;
        }

        public async Task<BatchDto> Create(BatchDto batchDto, CancellationToken cancellationToken = default)
        {
            Batch batch = batchDto.Adapt<Batch>();
            _repositoryManager.BatchRepository.Insert(batch);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            //EF hydrates this with auto-generated values like Id.
            return batch.Adapt<BatchDto>();
        }
    }
}
