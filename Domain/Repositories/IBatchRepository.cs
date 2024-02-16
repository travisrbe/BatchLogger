using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IBatchRepository : IRepositoryBase<Batch>
    {
        Task<IEnumerable<Batch>> GetAllAsync(CancellationToken cancellationToken);
        Task<Batch?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Batch?>> GetUserBatchesAsync(string userId, CancellationToken cancellationToken);
        void Insert(Batch batch);
        void Modify(Batch batch);
        void Remove(Batch batch);
    }
}
