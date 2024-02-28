using Contracts;

namespace Service.Abstractions
{
    public interface IBatchService
    {
        Task<IEnumerable<BatchDto?>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<BatchDto> GetByIdAsync(string userId, Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<BatchDto?>> GetByUserIdAsync(string id, bool isComplete, CancellationToken cancellationToken = default);
        Task<IEnumerable<BatchDto?>> GetByOwnedAsync(string id, CancellationToken cancellationToken= default);
        Task<BatchDto> Update(string userId, BatchDto batchDto, CancellationToken cancellationToken = default);
        Task<BatchDto> Create(BatchDto batchDto, CancellationToken cancellationToken = default);
        Task<BatchDto> Delete(string userId, BatchDto batchDto, CancellationToken cancellationToken = default);
    }
}
