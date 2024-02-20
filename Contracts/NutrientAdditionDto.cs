using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class NutrientAdditionDto
    {
        public Guid Id { get; set; }

        public double? MaxGramsPerLiterOverride { get; set; } = null;

        public double? YanPpmPerGramOverride { get; set; } = null;

        public double? EffectivenessMutiplierOverride { get; set; } = null;

        public Guid BatchId { get; set; }

        public Guid NutrientId { get; set; }
    }
}
