using Contracts;

namespace Service.Abstractions
{
    public interface IYeastService
    {
        Task<IEnumerable<YeastDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<YeastDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
