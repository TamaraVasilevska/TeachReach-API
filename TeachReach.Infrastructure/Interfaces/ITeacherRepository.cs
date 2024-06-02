using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Infrastructure.Interfaces
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetAllTeachersWithSubjectsAndReviews();
        Task<Teacher> GetTeacherByEmail(string email);

        Task<Teacher> GetTeacherWithSubjectsAndReviewsById(int id);
    }
}
