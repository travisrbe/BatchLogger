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

        public string NameOverride { get; set; } = string.Empty;

        public double? MaxGramsPerLiterOverride { get; set; } = null;

        public int? YanPpmPerGramOverride { get; set; }

        public double? EffectivenessMutiplierOverride { get; set; } = null;
        public double? GramsToAdd { get; set; }
        public int? YanPpmAdded { get; set; }
        public int Priority { get; set; }

        public Guid BatchId { get; set; }

        public Guid NutrientId { get; set; }
    }
}
