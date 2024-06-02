using System.ComponentModel.DataAnnotations;

namespace TeachReach.TeachReach.Domain.Entities
{
    public class Subject : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Teacher>? Teachers { get; set; } = new List<Teacher>();
    }
}
