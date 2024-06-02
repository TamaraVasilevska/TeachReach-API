using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Infrastructure.Interfaces
{
    public interface ISessionRepository
    {
        Task<List<Session>> GetAllSessionsWithStudentAndTeacher();

        Task<List<Session>> GetAllSessionsByTeacherId(int id);

        Task<Session> GetAllSessionsWithStudentAndTeacherById(int id);
    }
}
