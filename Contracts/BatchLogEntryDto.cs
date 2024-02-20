using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class BatchLogEntryDto
    {
        public Guid Id { get; set; }

        public string LogText { get; set; } = string.Empty;
        public double? SpecificGravityReading { get; set; }
        public double? pHReading { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public Guid BatchId { get; set; }
        public string UserId { get; set; } = null!;
        
    }
}
