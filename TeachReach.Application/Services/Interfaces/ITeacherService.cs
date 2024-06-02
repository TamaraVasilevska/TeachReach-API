using TeachReach.TeachReach.Application.ResponseModels;
using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Application.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetAllTeachers();
        Task<Teacher> GetById(int id);
        Task<Teacher> GetByEmail(string email);
        Task<SaveResponse> Create(Teacher teacher);
        Task<SaveResponse> Update(Teacher teacher);
        Task<SaveResponse> Delete(int id);
        
    }
}
