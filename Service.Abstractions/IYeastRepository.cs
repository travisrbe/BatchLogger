using Domain.Entities;

namespace Domain.Repositories
{
    public interface IYeastRepository : IRepositoryBase<Yeast>
    {
        Task<IEnumerable<Yeast>> GetAllAsync(CancellationToken cancellationToken);
        Task<Yeast?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}