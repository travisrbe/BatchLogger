namespace Domain.Exceptions
{
    public sealed class UserDoesNotOwnBatchException : BadRequestException
    {
        public UserDoesNotOwnBatchException(string userId, Guid batchId) : 
            base($"The user with identifier {userId} does not own the batch with identifier {batchId}")
        {

        }
    }
}
