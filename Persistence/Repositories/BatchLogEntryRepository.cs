﻿using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class BatchLogEntryRepository : RepositoryBase<BatchLogEntry>, IBatchLogEntryRepository
    {
        public BatchLogEntryRepository(DataContext context) : base(context) { }
        public async Task<IEnumerable<BatchLogEntry>> GetBatchLogEntries(string userID, Guid batchId, CancellationToken cancellationToken)
        {
            return await FindByCondition(x => x.BatchId == batchId && x.IsDeleted == false)
                .ToListAsync(cancellationToken);
        }
    }
}
