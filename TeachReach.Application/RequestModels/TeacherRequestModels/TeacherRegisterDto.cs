using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Application.RequestModels.TeacherRequestModels
{
    public class TeacherRegisterDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Bio { get; set; }
        public string? City { get; set; }
        public List<int>? SubjectId { get; set; } = new List<int>();
        public string PhoneNumber { get; set; }
        public int Experience { get; set; } = 0;
        public DateTime DateOfBirth { get; set; }
        public decimal HourlyRate { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
