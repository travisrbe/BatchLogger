using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Nutrient
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public double? MaxGramsPerLiter { get; set; }
        public int YanPpmPerGram { get; set; }
        public double EffectivenessMutiplier { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<NutrientAddition> NutrientAdditions { get; set; } = [];
    }
}
