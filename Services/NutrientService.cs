using Contracts;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Service.Abstractions;

namespace Services
{
    internal sealed class NutrientService : INutrientService
    {
        private readonly IRepositoryManager _repositoryManager;
        public NutrientService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<IEnumerable<NutrientDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var nutrients = await _repositoryManager.NutrientRepository.GetAllAsync(cancellationToken);
            var NutrientsDto = nutrients.Adapt<IEnumerable<NutrientDto>>();
            return NutrientsDto;
        }

        public async Task<NutrientDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var nutrient = await _repositoryManager.NutrientRepository.GetByIdAsync(id, cancellationToken);
            if (nutrient == null)
            {
                throw new NutrientNotFoundException(id);
            }
            var nutrientDto = nutrient.Adapt<NutrientDto>();
            return nutrientDto;
        }
    }
}
