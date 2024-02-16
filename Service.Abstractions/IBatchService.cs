using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface IBatchService
    {
        Task<IEnumerable<BatchDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<BatchDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BatchDto> Create(BatchDto batchDto, CancellationToken cancellationToken = default);
    }
}
