using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }
        public async Task<User?> GetUserIdFromCollaboratorToken(Guid collaboratorToken, CancellationToken cancellationToken)
        {
            return await FindByCondition(x => x.CollaboratorToken == collaboratorToken)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
