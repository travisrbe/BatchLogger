using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BatchLogEntry
    {
        public Guid Id { get; set; }

        [MaxLength(2048)]
        public string LogText { get; set; } = string.Empty;
        public double? SpecificGravityReading { get; set; }
        public double? pHReading { get; set; }
        public bool IsDataEntry { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public Guid BatchId { get; set; }
        public virtual Batch Batch { get; set; } = null!;

        [MaxLength(450)]
        public string UserId { get; set; } = null!;
        public virtual User User { get; set; } = null!;

    }
}
