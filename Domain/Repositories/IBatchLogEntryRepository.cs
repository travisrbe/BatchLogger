using Domain.Entities;

namespace Domain.Repositories
{
    public interface IBatchLogEntryRepository : IRepositoryBase<BatchLogEntry>
    {
        Task<IEnumerable<BatchLogEntry>> GetBatchLogEntries(string userID, Guid batchId, CancellationToken cancellationToken);
    }
}
