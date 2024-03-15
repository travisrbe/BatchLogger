using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NutrientAddition
    {
        public Guid Id { get; set; }

        public string NameOverride { get; set; } = string.Empty;
        public double? MaxGramsPerLiterOverride { get; set; }
        public double? YanPpmPerGramOverride { get; set; }
        public double? EffectivenessMutiplierOverride { get; set; }
        public double? GramsToAdd { get; set; }
        public int? YanPpmAdded { get; set; }
        public bool IsDeleted { get; set; }
        
        [Range(1, 10)]
        public int Priority { get; set; }

        public Guid BatchId { get; set; }
        public virtual Batch Batch { get; set; } = null!;

        public Guid NutrientId { get; set; }
        public virtual Nutrient Nutrient { get; set; } = null!;
    }
}