using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Batch
    {
        //database generated
        public Guid Id { get; set; }

        //required inputs
        public double SpecificGravity { get; set; }
        public int OffsetYanPpm { get; set; }
        public double VolumeLiters { get; set; }

        //optional inputs
        public string Ingredients { get; set; } = string.Empty;
        public string Process { get; set; } = string.Empty;

        //calculated
        public double? Brix { get; set; }
        public double? SugarPpm { get; set; }
        public double? SubtotalYanPpm {get; set;}
        public double? TotalTargetYanPpm { get; set; }
        public double? RemainderPpmNeeded { get; set; }
        public int? RemainderNutrientId { get; set; }
        public double? RemainderNutrientGrams { get; set; }
        public bool Complete { get; set; } = false;
        public bool IsDeleted { get; set; }

        public String CreatorUserId { get; set; } = string.Empty;
        public virtual User Creator { get; set; } = null!;

        public Guid YeastId { get; set; }
        public virtual Yeast Yeast { get; set; } = null!;

        public virtual ICollection<BatchLogEntry> LogEntries { get; set; } = [];
        public virtual ICollection<NutrientAddition> NutrientAdditions { get; set; } = [];
        public virtual ICollection<UserBatch> UserBatches { get; set; } = null!;
    }
}
