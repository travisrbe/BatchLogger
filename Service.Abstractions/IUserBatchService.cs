using Contracts;

namespace Service.Abstractions
{
    public interface IUserBatchService
    {
        Task<UserBatchDto> Create(string userId, Guid batchId, Guid collaboratorToken = new Guid(), CancellationToken cancellationToken = default);
        Task<UserBatchDto> Delete(string userId, UserBatchDto userBatchDto, CancellationToken cancellationToken = default);
    }
}
