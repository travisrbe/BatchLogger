using Contracts;

namespace Service.Abstractions
{
    public interface IUserBatchService
    {
        Task<UserBatchDto> Create(string userId, Guid batchId, CancellationToken cancellationToken = default);
        Task<UserBatchDto> Delete(string userId, UserBatchDto userBatchDto, CancellationToken cancellationToken = default);
    }
}
