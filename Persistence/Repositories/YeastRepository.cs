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
    public class YeastRepository : RepositoryBase<Yeast>, IYeastRepository
    {
        public YeastRepository(DataContext context) : base(context) {}

        public async Task<IEnumerable<Yeast>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await FindAll().OrderBy(y => y.Name).ToListAsync(cancellationToken);
        }

        public async Task<Yeast?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await FindByCondition(y => y.Id == id).SingleOrDefaultAsync(cancellationToken);
        }

        public void Insert(Yeast yeast)
        {
            Create(yeast);
        }
        public void Modify(Yeast yeast)
        {
            Update(yeast); ;
        }

        public void Remove(Yeast yeast)
        {
            Delete(yeast);
        }
    }
}
