namespace TeachReach.TeachReach.Domain.Entities
{
    public class Review : BaseEntity
    {
        public Teacher Teacher { get; set; }
        public int? TeacherId { get; set; }
        public Student Student { get; set; }
        public int? StudentId { get; set; }
        public int? Rating { get; set; } 
        public string? Comment { get; set; } 
        public DateTime DateCreated { get; set; } 
    }
}
