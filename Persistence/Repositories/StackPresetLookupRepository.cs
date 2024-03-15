using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    internal class StackPresetLookupRepository : RepositoryBase<StackPresetLookup>, IStackPresetLookupRepository
    {
        public StackPresetLookupRepository(DataContext context) : base(context) { }
    }
}
