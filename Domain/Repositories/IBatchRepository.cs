using Domain.Entities;

namespace Domain.Repositories
{
    public interface IBatchRepository : IRepositoryBase<Batch>
    {
        Task<IEnumerable<Batch>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> UserOwnsBatch(string userId, Guid batchId, CancellationToken cancellationToken);
        Task<bool> UserContributesBatch(string userId, Guid batchId, CancellationToken cancellationToken);
        Task<Batch?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Batch?>> GetUserBatchesAsync(string userId, CancellationToken cancellationToken);
        Task<IEnumerable<Batch?>> GetOwnedBatchesAsync(string userId, CancellationToken cancellationToken);
    }
}
