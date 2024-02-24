using Contracts;

namespace Service.Abstractions
{
    public interface INutrientService
    {
        Task<IEnumerable<NutrientDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<NutrientDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
