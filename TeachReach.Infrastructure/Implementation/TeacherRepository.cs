using Microsoft.EntityFrameworkCore;
using TeachReach.TeachReach.Domain.Entities;
using TeachReach.TeachReach.Infrastructure.Interfaces;

namespace TeachReach.TeachReach.Infrastructure.Implementation
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly TeachReachDbContext _context;

        public TeacherRepository(TeachReachDbContext context)
        {
            _context = context;
        }

        public async Task<List<Teacher>> GetAllTeachersWithSubjectsAndReviews()
        {
            return await _context.Teachers
                 .Include(t => t.Subjects)
                 .Include(t => t.Reviews)
                 .ToListAsync();
        }

        public async Task<Teacher> GetTeacherByEmail(string email)
        {
            return await _context.Teachers.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Teacher> GetTeacherWithSubjectsAndReviewsById(int id)
        {
            return await _context.Teachers
                 .Include(t => t.Subjects)
                 .Include(t => t.Reviews)
                 .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
