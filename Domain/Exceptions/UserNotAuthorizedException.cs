using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class UserNotAuthorizedException : BadRequestException
    {
        public UserNotAuthorizedException(string userId, Guid batchId) :
            base($"The user with identifier {userId} does not own batch with identifier {batchId} and is not otherwise authorized to perform this action.")
        {

        }
    }
}
