using TeachReach.TeachReach.Application.ResponseModels;
using TeachReach.TeachReach.Application.Services.Interfaces;
using TeachReach.TeachReach.Domain.Entities;
using TeachReach.TeachReach.Infrastructure.Interfaces;

namespace TeachReach.TeachReach.Application.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IStudentRepository _customStudentRepository;
        public StudentService(IGenericRepository<Student> studentRepository, IStudentRepository customStudentRepository)
        {
            _studentRepository = studentRepository;
            _customStudentRepository = customStudentRepository;
        }

        public async Task<SaveResponse> Create(Student student)
        {
            await _studentRepository.Create(student);
            return new SaveResponse() { Success = true };
        }

        public async Task<SaveResponse> Delete(int id)
        {
            var studentToDelete = await _studentRepository.GetById(id);
            if (studentToDelete == null)
            {
                throw new Exception($"Student not found");
            }
            await _studentRepository.Delete(studentToDelete);
            return new SaveResponse() { Success = true };
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _studentRepository.GetAll();
        }
        public async Task<Student> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
            }

            var student = await _customStudentRepository.GetStudentWithEmail(email);

            if (student == null)
            {
                throw new Exception($"Student with email '{email}' not found.");
            }

            return student;
        }

        public async Task<Student> GetById(int id)
        {
            return await _studentRepository.GetById(id);
        }

        public async Task<SaveResponse> Update(Student student)
        {
            var existingStudent = await _studentRepository.GetById(student.Id);

            if (existingStudent == null)
            {
                throw new Exception($"Student with ID {student.Id} not found.");
            }
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Email = student.Email;

            await _studentRepository.Update(existingStudent);

            return new SaveResponse { Success = true };
        }
    }
}
