using System.ComponentModel.DataAnnotations;

namespace TeachReach.TeachReach.Domain.Entities
{
    public class Session : BaseEntity
    {
       
        public Teacher? Teacher { get; set; }
        public int? TeacherId { get; set; }
       
        public Student? Student { get; set; }
        public int? StudentId { get; set; } 
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; }
       
        public Subject? Subject { get; set; }
        public int? SubjectId { get; set; }
        public string? Notes { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
