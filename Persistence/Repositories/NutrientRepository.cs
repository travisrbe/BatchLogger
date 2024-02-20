using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class NutrientRepository : RepositoryBase<Nutrient>, INutrientRepository
    {
        public NutrientRepository(DataContext context) : base(context) { }
        public async Task<IEnumerable<Nutrient>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await FindByCondition(x => x.IsDeleted == false)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<Nutrient?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await FindByCondition(n => n.Id == id && n.IsDeleted == false)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
