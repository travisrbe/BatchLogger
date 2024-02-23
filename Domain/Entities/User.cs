using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public User() { }

        [MaxLength(128)]
        public string ChosenName { get; set; } = string.Empty;
        [MaxLength(128)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(128)]
        public string LastName { get; set; } = string.Empty;
        public Guid CollaboratorToken { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Batch> CreatedBatches { get; set; } = null!;
        public virtual ICollection<BatchLogEntry> LogEntries { get; set; } = null!;
        public virtual ICollection<UserBatch> UserBatches { get; set; } = null!;
    }
}
