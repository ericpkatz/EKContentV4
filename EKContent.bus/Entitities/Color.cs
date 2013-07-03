using ekUtilities;

namespace EKContent.bus.Entities
{
    public class Color : BaseEntity
    {
        public bool IsPalette { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}