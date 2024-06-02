using TeachReach.TeachReach.Application.ResponseModels;
using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Application.Services.Interfaces
{
    public interface ISessionService
    {
        Task<List<Session>> GetAllSessions();
        Task<List<Session>> GetAllSessionsByTeacherId(int id);
        Task<Session> GetById(int id);
        Task<SaveResponse> Create(Session session);
        Task<SaveResponse> Update(Session session);
        Task<SaveResponse> Delete(int id);
    }
}
