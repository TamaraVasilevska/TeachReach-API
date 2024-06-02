using TeachReach.TeachReach.Application.ResponseModels;
using TeachReach.TeachReach.Application.Services.Interfaces;
using TeachReach.TeachReach.Domain.Entities;
using TeachReach.TeachReach.Infrastructure.Interfaces;

namespace TeachReach.TeachReach.Application.Services.Implementation
{
    public class SessionService : ISessionService
    {
        private readonly IGenericRepository<Session> _SessionRepository;
        private readonly ISessionRepository _customSessionRepository;

        public SessionService(IGenericRepository<Session> SessionRepository, ISessionRepository customSessionRepository)
        {
            _SessionRepository = SessionRepository;
            _customSessionRepository = customSessionRepository;
        }
        public async Task<SaveResponse> Create(Session Session)
        {
            await _SessionRepository.Create(Session);
            return new SaveResponse() { Success = true };
        }

        public async Task<SaveResponse> Delete(int id)
        {
            var SessionToDelete = await _SessionRepository.GetById(id);
            if (SessionToDelete == null)
            {
                throw new Exception($"Session not found");
            }
            await _SessionRepository.Delete(SessionToDelete);
            return new SaveResponse() { Success = true };
        }

        public async Task<List<Session>> GetAllSessions()
        {
            return await _customSessionRepository.GetAllSessionsWithStudentAndTeacher();
        }

        public async Task<List<Session>> GetAllSessionsByTeacherId(int id)
        {
            return await _customSessionRepository.GetAllSessionsByTeacherId(id);
        }

        public async Task<Session> GetById(int id)
        {
            return await _customSessionRepository.GetAllSessionsWithStudentAndTeacherById(id);
        }

        public async Task<SaveResponse> Update(Session session)
        {
            var existingSession = await _SessionRepository.GetById(session.Id);

            if (existingSession == null)
            {
                throw new Exception($"Session with ID {session.Id} not found.");
            }

            existingSession.StartTime = session.StartTime;
            existingSession.EndTime = session.EndTime;
            existingSession.TeacherId = session.TeacherId;
            existingSession.StudentId = session.StudentId;
            existingSession.SubjectId = session.SubjectId;
            existingSession.Notes = session.Notes;
            existingSession.IsCompleted = session.IsCompleted;


            return new SaveResponse { Success = true };
        }
    }
}
