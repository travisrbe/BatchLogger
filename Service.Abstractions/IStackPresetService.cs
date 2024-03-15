using Contracts;

namespace Service.Abstractions
{
    public interface IStackPresetService
    {
        Task<IEnumerable<StackPresetDto>> GetOrderedStackPresets(CancellationToken cancellationToken);
    }
}
