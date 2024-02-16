using System;

namespace Domain.Exceptions
{
    public sealed class BatchNotFoundException : NotFoundException
    {
        public BatchNotFoundException(Guid id) : base($"The batch with the identifier {id} was not found")
        {
        }
    }
}