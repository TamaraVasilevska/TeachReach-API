using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Infrastructure.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentWithEmail(string email);
    }
}
