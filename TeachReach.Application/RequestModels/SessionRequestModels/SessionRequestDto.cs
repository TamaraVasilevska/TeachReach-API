using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Application.RequestModels.SessionRequestModels
{
    public class SessionRequestDto
    {
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? SubjectId { get; set; }
        public string? Notes { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
