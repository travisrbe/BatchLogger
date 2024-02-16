using Domain.Entities;

namespace Domain.Repositories
{
    public interface IYeastRepository : IRepositoryBase<Yeast>
    {
        Task<IEnumerable<Yeast>> GetAllAsync(CancellationToken cancellationToken);
        Task<Yeast?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        void Insert(Yeast yeast);
        void Modify(Yeast yeast);
        void Remove(Yeast yeast);
    }
}