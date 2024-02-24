using Contracts;

namespace Service.Abstractions
{
    public interface IBatchLogEntryService
    {
        Task<BatchLogEntryDto> Create(string userId, BatchLogEntryDto batchLogEntryDto, CancellationToken cancellationToken = default);
        Task<IEnumerable<BatchLogEntryDto>> GetBatchLogEntries(string userId, Guid batchId, CancellationToken cancellationToken = default);
        Task<BatchLogEntryDto> Update(string userId, BatchLogEntryDto batchLogEntryDto, CancellationToken cancellationToken = default);
        Task<BatchLogEntryDto> Delete(string userId, BatchLogEntryDto batchLogEntryDto, CancellationToken cancellationToken = default);
    }
}