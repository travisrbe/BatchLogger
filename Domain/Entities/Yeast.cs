using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Yeast
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double NutrientReqMult { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Batch> Batches { get; set; } = null!;
    }
}
