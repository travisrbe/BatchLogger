using System;

namespace Domain.Exceptions
{
    public sealed class YeastNotFoundException : NotFoundException
    {
        public YeastNotFoundException(Guid id) : base($"The yeast with the identifier {id} was not found")
        {
        }
    }
}
