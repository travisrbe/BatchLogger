namespace Domain.Exceptions
{
    public sealed class BatchLogEntryNotFoundException : NotFoundException
    {
        public BatchLogEntryNotFoundException(Guid id) : base($"The batch log entry with the identifier {id} was not found.")
        {
        }
    }
}