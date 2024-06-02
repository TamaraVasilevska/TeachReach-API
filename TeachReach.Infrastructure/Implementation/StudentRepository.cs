using Microsoft.EntityFrameworkCore;
using TeachReach.TeachReach.Domain.Entities;
using TeachReach.TeachReach.Infrastructure.Interfaces;

namespace TeachReach.TeachReach.Infrastructure.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly TeachReachDbContext _context;

        public StudentRepository(TeachReachDbContext context)
        {
            _context = context;
        }
        public async Task<Student> GetStudentWithEmail(string email)
        {
            return await _context.Students.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
