using TeachReach.TeachReach.Application.ResponseModels;
using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Application.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubjects();
        Task<Subject> GetById(int id);
        Task<List<Subject>> GetAllSubjectsByTeacherId(int id);
        Task<SaveResponse> Create(Subject subject);
        Task<SaveResponse> Update(Subject subject);
        Task<SaveResponse> Delete(int id);

    }
}
