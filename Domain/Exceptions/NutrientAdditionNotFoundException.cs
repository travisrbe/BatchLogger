namespace Domain.Exceptions
{
    public sealed class NutrientAdditionNotFoundException : NotFoundException
    {
        public NutrientAdditionNotFoundException(Guid id) : base($"The nutrient addition with the identifier {id} was not found.")
        {
        }
    }
}
