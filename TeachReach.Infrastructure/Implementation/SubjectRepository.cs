using System.Collections.Generic;
using System.Threading.Tasks;
using TeachReach.TeachReach.Domain.Entities;
using TeachReach.TeachReach.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore; 
using System.Linq;

namespace TeachReach.TeachReach.Infrastructure.Implementation
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly TeachReachDbContext _context;

        public SubjectRepository(TeachReachDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Subject>> GetAllSubjectsByTeacherId(int id)
        {

            var subjects = await _context.Subjects
                 .Where(s => s.Teachers.Any(t => t.Id == id))
                 .ToListAsync();

            return subjects;
        }
    }
}
