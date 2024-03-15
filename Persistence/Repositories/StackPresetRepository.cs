using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public class StackPresetRepository : RepositoryBase<StackPreset>, IStackPresetRepository
    {
        public StackPresetRepository(DataContext context) : base(context) { }
    }
}
