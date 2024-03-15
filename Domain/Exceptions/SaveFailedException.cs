namespace Domain.Exceptions
{
    public sealed class SaveFailedException : Exception
    {
        public SaveFailedException(Guid id) : base($"Saving for object with ID: {id} failed. Save was aborted.")
        {
        }
    }
}