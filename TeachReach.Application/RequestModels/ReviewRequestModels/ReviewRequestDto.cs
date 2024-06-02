using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Application.RequestModels.ReviewRequestModels
{
    public class ReviewRequestDto
    {
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
