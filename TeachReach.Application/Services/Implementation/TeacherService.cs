using TeachReach.TeachReach.Application.ResponseModels;
using TeachReach.TeachReach.Application.Services.Interfaces;
using TeachReach.TeachReach.Domain.Entities;
using TeachReach.TeachReach.Infrastructure.Interfaces;

namespace TeachReach.TeachReach.Application.Services.Implementation
{
    public class TeacherService : ITeacherService
    {
        private readonly IGenericRepository<Teacher> _teacherRepository;
        private readonly ITeacherRepository _customTeacherRepository;

        public TeacherService(IGenericRepository<Teacher> teacherRepository, ITeacherRepository customTeacherRepository)
        {
            _customTeacherRepository = customTeacherRepository;
            _teacherRepository = teacherRepository;
        }
        public async Task<SaveResponse> Create(Teacher teacher)
        {
            await _teacherRepository.Create(teacher);
            return new SaveResponse() { Success = true };
        }

        public async Task<SaveResponse> Delete(int id)
        {
            var teacherToDelete = await _teacherRepository.GetById(id);
            if (teacherToDelete == null)
            {
                throw new Exception($"Teacher not found.");
            }
            await _teacherRepository.Delete(teacherToDelete);
            return new SaveResponse() { Success = true };
        }

        public async Task<List<Teacher>> GetAllTeachers()
        {
            return await _customTeacherRepository.GetAllTeachersWithSubjectsAndReviews();
        }

        public async Task<Teacher> GetByEmail(string email)
        {
            return await _customTeacherRepository.GetTeacherByEmail(email);
        }

        public async Task<Teacher> GetById(int id)
        {
            return await 
                _customTeacherRepository.GetTeacherWithSubjectsAndReviewsById(id);
        }

        public async Task<SaveResponse> Update(Teacher teacher)
        {
                var existingTeacher = await _teacherRepository.GetById(teacher.Id);


                if (existingTeacher == null)
                {
                throw new Exception($"Teacher not found.");
                }

                existingTeacher.FirstName = teacher.FirstName;
                existingTeacher.LastName = teacher.LastName;
                existingTeacher.Email = teacher.Email;
                existingTeacher.Bio = teacher.Bio;
                existingTeacher.City = teacher.City;
                existingTeacher.PhoneNumber = teacher.PhoneNumber;
                existingTeacher.Experience = teacher.Experience;
                existingTeacher.DateOfBirth = teacher.DateOfBirth;
                existingTeacher.HourlyRate = teacher.HourlyRate;
                existingTeacher.ProfilePictureUrl = teacher.ProfilePictureUrl;

                await _teacherRepository.Update(existingTeacher);

                return new SaveResponse() { Success = true };
        }
    }
}
