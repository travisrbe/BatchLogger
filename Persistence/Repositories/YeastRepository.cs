using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class YeastRepository : RepositoryBase<Yeast>, IYeastRepository
    {
        public YeastRepository(DataContext context) : base(context) {}

        public async Task<Yeast?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await FindByCondition(y => y.Id == id && y.IsDeleted == false)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Yeast>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await FindByCondition(y => y.IsDeleted == false)
                .OrderBy(y => y.Brand).ThenBy(y => y.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
