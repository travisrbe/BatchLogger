using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface INutrientService
    {
        Task<IEnumerable<NutrientDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<NutrientDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
