using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UserBatchAlreadyExistsException : AlreadyExistsException
    {
        public UserBatchAlreadyExistsException(string userId, Guid batchId)
            : base($"The user with identifier {userId} is already a contributor to batch with identifier {batchId}")
        {

        }
    }
}
