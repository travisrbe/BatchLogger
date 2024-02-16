using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface IYeastService
    {
        Task<IEnumerable<YeastDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<YeastDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
