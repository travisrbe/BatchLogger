using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class NutrientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public double? MaxGramsPerLiter { get; set; }
        public int YanPpmPerGram { get; set; }
        public double EffectivenessMutiplier { get; set; }
    }
}
