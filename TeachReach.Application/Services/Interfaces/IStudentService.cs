using TeachReach.TeachReach.Application.ResponseModels;
using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Application.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> GetById(int id);
        Task<Student> GetByEmail(string email);
        Task<SaveResponse> Create(Student student);
        Task<SaveResponse> Update(Student student);
        Task<SaveResponse> Delete(int id);
    }
}
