using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BatchLogEntry
    {
        public Guid Id { get; set; }

        public string LogText { get; set; } = string.Empty;
        public double? SpecificGravityReading { get; set; }
        public double? pHReading { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public Guid BatchId { get; set; }
        public virtual Batch Batch { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public virtual User User { get; set; } = null!;

    }
}
