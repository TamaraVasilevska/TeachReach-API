using Microsoft.AspNetCore.Mvc;
using TeachReach.TeachReach.Application.Services.Implementation;
using TeachReach.TeachReach.Application.Services.Interfaces;
using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet("subjects")]
        public async Task<List<Subject>> GetSubjects()
        {

            return await _subjectService.GetAllSubjects();
        }

        [HttpGet("subject/{id}")]
        public async Task<ActionResult<Subject>> Get(int id)
        {
            return await _subjectService.GetById(id);
        }

        [HttpDelete("subject/{id}")]
        public async Task<IActionResult> DeleteSubject([FromRoute] int id)
        {
            try
            {
                var response = await _subjectService.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("subject")]
        public async Task<IActionResult> CreateSubject(Subject newSubject)
        {
            try
            {
                var response = await _subjectService.Create(newSubject);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("subject/{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] Subject subject)
        {
            try
            {
                if (id != subject.Id)
                {
                    return BadRequest("Subject ID in the URL does not match the ID in the request body.");
                }

                var response = await _subjectService.Update(subject);

                if (response.Success)
                {
                    return Ok("Subject updated successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to update subject.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("teacher/{teacherId}/subjects")]
        public async Task<ActionResult<List<Subject>>> GetSubjectsByTeacherId(int teacherId)
        {
            try
            {
                var subjects = await _subjectService.GetAllSubjectsByTeacherId(teacherId);
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
