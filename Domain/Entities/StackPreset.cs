using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class StackPreset
    {
        public StackPreset() { }

        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Order { get; set; }

        public virtual ICollection<StackPresetLookup> StackPresetLookups { get; set; } = [];

    }
}
