using TeachReach.TeachReach.Application.ResponseModels;
using TeachReach.TeachReach.Application.Services.Interfaces;
using TeachReach.TeachReach.Domain.Entities;
using TeachReach.TeachReach.Infrastructure.Interfaces;

namespace TeachReach.TeachReach.Application.Services.Implementation
{
    public class SubjectService : ISubjectService
    {
        private readonly IGenericRepository<Subject> _subjectRepository;
        private readonly ISubjectRepository _customSubjectRepository;

        public SubjectService(IGenericRepository<Subject> subjectRepository, ISubjectRepository customSubjectRepository)
        {
            _subjectRepository = subjectRepository;
            _customSubjectRepository = customSubjectRepository;
        }

        public async Task<SaveResponse> Create(Subject subject)
        {
            await _subjectRepository.Create(subject);
            return new SaveResponse() { Success = true };
        }

        public async Task<SaveResponse> Delete(int id)
        {
            var subjectToDelete= await _subjectRepository.GetById(id);
            if(subjectToDelete == null) 
            {
                throw new Exception($"Subject not found");
            }
            await _subjectRepository.Delete(subjectToDelete);
            return new SaveResponse() { Success = true };
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            return await _subjectRepository.GetAll();
        }

        public async Task<List<Subject>> GetAllSubjectsByTeacherId(int id)
        {
            return await _customSubjectRepository.GetAllSubjectsByTeacherId(id);
        }

        public async Task<Subject> GetById(int id)
        {
            return await _subjectRepository.GetById(id);
        }

        public async Task<SaveResponse> Update(Subject subject)
        {
            var existingSubject = await _subjectRepository.GetById(subject.Id);

            if (existingSubject == null)
            {
                throw new Exception($"Subject with ID {subject.Id} not found.");
            }

            existingSubject.Name = subject.Name;
            existingSubject.Description = subject.Description;

            await _subjectRepository.Update(existingSubject);

            return new SaveResponse { Success = true };
        }



    }
}
