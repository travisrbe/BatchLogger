namespace Domain.Exceptions
{
    public sealed class NutrientNotFoundException : NotFoundException
    {
        public NutrientNotFoundException(Guid id) : base($"The nutrient with the identifier {id} was not found.")
        {
        }
    }
}