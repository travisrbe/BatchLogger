﻿using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BatchRepository : RepositoryBase<Batch>, IBatchRepository
    {
        public BatchRepository(DataContext context) : base(context) { }
        public async Task<bool> UserOwnsBatch (string userId, Guid batchId, CancellationToken cancellationToken)
        {
            Batch? batch = await FindByCondition(x => x.Id == batchId && x.IsDeleted == false)
                .SingleOrDefaultAsync(cancellationToken) 
                ?? throw new BatchNotFoundException(batchId);
            return batch.OwnerUserId == userId;
        }
        public async Task<bool> UserContributesBatch(string userId, Guid batchId, CancellationToken cancellationToken)
        {
            Batch? batch = await FindByCondition(x => x.Id == batchId && x.IsDeleted == false)
                .Include(x => x.UserBatches
                    .Where(b => b.IsDeleted == false))
                .SingleOrDefaultAsync() 
                ?? throw new BatchNotFoundException(batchId);

            if (batch.UserBatches.Where(x => x.UserId == userId && x.IsDeleted == false).FirstOrDefault() == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Batch>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await FindAll().OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }
        public async Task<Batch?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await FindByCondition(x => x.Id == id && x.IsDeleted == false)
                .Include(b => b.Yeast)
                .Include(b => b.UserBatches
                    .Where(ub => ub.IsDeleted == false))
                .Include(b => b.LogEntries
                    .Where(le => le.IsDeleted == false))
                .Include(b => b.NutrientAdditions
                    .Where(na => na.IsDeleted == false))
                .SingleOrDefaultAsync(cancellationToken)
                ?? throw new BatchNotFoundException(id);
        }

        public async Task<IEnumerable<Batch?>> GetUserBatchesAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _context.Batches
                .Where(b => b.UserBatches
                    .Any(x => x.UserId == userId && x.IsDeleted == false)
                ).ToListAsync(cancellationToken);
            return result;
        }

        public async Task<IEnumerable<Batch?>> GetOwnedBatchesAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _context.Batches
                .Where(b => b.OwnerUserId == userId && b.IsDeleted == false)
                .ToListAsync(cancellationToken);
            return result;
        }

        new public void Update(Batch batch)
        {
            _context.Update(batch);
        }
    }
}
