using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Batch>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await FindAll().OrderBy(x => x.Id).ToListAsync(cancellationToken);
        }
        public async Task<Batch?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await FindByCondition(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);
        }
        public async Task<IEnumerable<Batch?>> GetUserBatchesAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _context.Batches
                .SelectMany(b => b.UserBatches)
                .Where(ub => ub.UserId == userId)
                .ToListAsync(cancellationToken);

            var result2 = await _context.Batches
                .Where(b => b.UserBatches
                    .Any(x => x.UserId == userId))
                .ToListAsync();
            return result2;
        }
        public void Insert(Batch batch)
        {
            Create(batch);
        }
        public void Modify(Batch batch)
        {
            Update(batch);
        }

        public void Remove(Batch batch)
        {
            Delete(batch);
        }
    }
}
