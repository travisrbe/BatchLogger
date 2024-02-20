using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserBatchRepository : IRepositoryBase<UserBatch>
    {
        Task<IEnumerable<UserBatch?>> GetByIdAsync(string id, CancellationToken cancellationToken);
    }
}
