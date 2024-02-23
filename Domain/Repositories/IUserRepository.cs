using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetUserIdFromCollaboratorToken(Guid collaboratorToken, CancellationToken cancellationToken);
    }
}
