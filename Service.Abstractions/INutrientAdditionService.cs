using Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface INutrientAdditionService
    {
        Task<NutrientAdditionDto> Create (string userId, NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken);
        Task<IEnumerable<NutrientAdditionDto>> GetByBatchId(string userId, Guid batchId, CancellationToken cancellationToken);
        Task<NutrientAdditionDto> Update(string userId, NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken);
        Task<NutrientAdditionDto> Delete(string userId, NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken);
    }
}
