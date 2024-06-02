using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Application.ResponseModels.TeacherResponses
{
    public class GetAllTeachersResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Bio { get; set; }
        public string? City { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public string PhoneNumber { get; set; }
        public int Experience { get; set; } = 0;
        public DateTime DateOfBirth { get; set; }
        public decimal HourlyRate { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
