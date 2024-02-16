using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public string ChosenName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Guid CollaboratorToken { get; set; } = new Guid();
        public bool IsDeleted { get; set; }

        public virtual ICollection<Batch> CreatedBatches { get; set; } = null!;
        public virtual ICollection<BatchLogEntry> LogEntries { get; set; } = null!;
        public virtual ICollection<UserBatch> UserBatches { get; set; } = null!;
    }
}
