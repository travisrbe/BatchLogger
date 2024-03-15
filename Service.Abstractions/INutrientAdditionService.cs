using Contracts;

namespace Service.Abstractions
{
    public interface INutrientAdditionService
    {
        Task<NutrientAdditionDto> Create (string userId, Guid batchId, Guid nutrientId, int priority, CancellationToken cancellationToken);
        Task<IEnumerable<NutrientAdditionDto>> GetByBatchId(string userId, Guid batchId, CancellationToken cancellationToken);
        Task<NutrientAdditionDto> Update(string userId, NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken);
        Task<IEnumerable<NutrientAdditionDto>> UpdateRange(string userId, 
            IEnumerable<NutrientAdditionDto> nutrientAdditionDtos, 
            CancellationToken cancellationToken);
        Task<IEnumerable<NutrientAdditionDto>> Reset(string userId, 
            IEnumerable<NutrientAdditionDto> nutrientAdditionDtos, 
            CancellationToken cancellationToken);
        Task<IEnumerable<NutrientAdditionDto>> SetStackPreset(string userId, Guid batchId, Guid stackPresetId, CancellationToken cancellationToken);
        Task<NutrientAdditionDto> RestoreDefaultValues(string userId, NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken);
        Task Delete(string userId, NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken);
    }
}
