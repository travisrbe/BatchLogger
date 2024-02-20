using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
