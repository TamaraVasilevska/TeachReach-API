using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Infrastructure.Interfaces
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllSubjectsByTeacherId(int id);
    }
}
