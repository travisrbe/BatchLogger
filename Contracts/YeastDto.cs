using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class YeastDto
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double NutrientReqMult { get; set; }
    }
}
