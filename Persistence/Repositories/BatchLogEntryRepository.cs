using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class BatchLogEntryRepository : RepositoryBase<BatchLogEntry>, IBatchLogEntryRepository
    {
        public BatchLogEntryRepository(DataContext context) : base(context) { }
    }
}
