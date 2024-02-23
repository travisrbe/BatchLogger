using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserBatch
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }

        [MaxLength(450)]
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        public Guid BatchId { get; set; }
        public Batch Batch { get; set; } = null!;
    }
}