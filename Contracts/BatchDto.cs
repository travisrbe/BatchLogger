using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class BatchDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double SpecificGravity { get; set; }
        public int OffsetYanPpm { get; set; }
        public double VolumeLiters { get; set; }
        public string Ingredients { get; set; } = string.Empty;
        public string Process { get; set; } = string.Empty;
        public bool GoFermUsed { get; set; } = true;
        public double? Brix { get; set; }
        public double? SugarPpm { get; set; }
        public int? SubtotalYanPpm { get; set; }
        public int? TotalTargetYanPpm { get; set; }
        public int? RemainderPpmNeeded { get; set; }
        public int? RemainderNutrientId { get; set; }
        public double? RemainderNutrientGrams { get; set; }
        public bool IsComplete { get; set; } = false;
        public bool IsLocked { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public String OwnerUserId { get; set; } = null!;
        public Guid? YeastId { get; set; }
    }
}
