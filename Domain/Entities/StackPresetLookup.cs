using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StackPresetLookup(Guid stackPresetId, Guid nutrientId)
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public Guid StackPresetId { get; set; } = stackPresetId;
        public virtual StackPreset StackPreset { get; set; } = null!;
        [Required]
        public Guid NutrientId { get; set; } = nutrientId;
        public virtual Nutrient Nutrient { get; set; } = null!;
    }
}
