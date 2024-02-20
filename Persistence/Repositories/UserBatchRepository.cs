using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserBatchRepository : RepositoryBase<UserBatch>, IUserBatchRepository
    {
        public UserBatchRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<UserBatch?>> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await FindByCondition(x => x.UserId == id && x.IsDeleted == false)
                .ToListAsync(cancellationToken);
        }
    }
}
