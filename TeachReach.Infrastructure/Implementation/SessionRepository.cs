using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeachReach.TeachReach.Domain.Entities;
using TeachReach.TeachReach.Infrastructure.Interfaces;

namespace TeachReach.TeachReach.Infrastructure.Implementation
{
    public class SessionRepository : ISessionRepository
    {
        private readonly TeachReachDbContext _context;

        public SessionRepository(TeachReachDbContext context)
        {
            _context = context;
        }

        public async Task<List<Session>> GetAllSessionsByTeacherId(int id)
        {
            return await _context.Sessions
                .Where(s => s.TeacherId == id)
                .Include(s => s.Teacher)
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .ToListAsync();
        }

        public async Task<List<Session>> GetAllSessionsWithStudentAndTeacher()
        {
            return await _context.Sessions
                .Include(s => s.Teacher)
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .ToListAsync();
        }

        public async Task<Session> GetAllSessionsWithStudentAndTeacherById(int id)
        {
            return await _context.Sessions
                .Include(s => s.Teacher)
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
