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
        public double SpecificGravity { get; set; }
        public int OffsetYanPpm { get; set; }
        public double VolumeLiters { get; set; }
        public string Ingredients { get; set; } = string.Empty;
        public string Process { get; set; } = string.Empty;
        public double? Brix { get; set; }
        public double? SugarPpm { get; set; }
        public double? SubtotalYanPpm { get; set; }
        public double? TotalTargetYanPpm { get; set; }
        public double? RemainderPpmNeeded { get; set; }
        public int? RemainderNutrientId { get; set; }
        public double? RemainderNutrientGrams { get; set; }
        public bool Complete { get; set; } = false;
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public String OwnerUserId { get; set; } = null!;
        public Guid? YeastId { get; set; }
    }
}
