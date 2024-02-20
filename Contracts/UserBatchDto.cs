using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class UserBatchDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid BatchId { get; set; }
    }
}
