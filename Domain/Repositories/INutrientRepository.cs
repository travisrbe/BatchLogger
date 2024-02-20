using Domain.Entities;

namespace Domain.Repositories
{
    public interface INutrientRepository : IRepositoryBase<Nutrient>
    {
        Task<IEnumerable<Nutrient>> GetAllAsync(CancellationToken cancellationToken);
        Task<Nutrient?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
