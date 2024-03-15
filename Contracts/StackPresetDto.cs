using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class StackPresetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order;
    }
}
