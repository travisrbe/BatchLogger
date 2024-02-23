using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Nutrient
    {
        public Nutrient(string name, string manufacturer) 
        {
            Name = name;
            Manufacturer = manufacturer;
        }
        public Guid Id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Manufacturer { get; set; }
        public double? MaxGramsPerLiter { get; set; }
        public int YanPpmPerGram { get; set; }
        public double EffectivenessMutiplier { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<NutrientAddition> NutrientAdditions { get; set; } = [];
    }
}
