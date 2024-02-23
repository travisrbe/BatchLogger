using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class UserCollaborationTokenNotFoundException : NotFoundException
    {
        public UserCollaborationTokenNotFoundException(Guid id) :
            base($"No user was found for collaboration token {id}.")
        {

        }
    }
}
