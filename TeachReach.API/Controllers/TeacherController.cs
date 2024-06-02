using Microsoft.AspNetCore.Mvc;
using TeachReach.TeachReach.Application.ResponseModels.TeacherResponses;
using TeachReach.TeachReach.Application.Services.Implementation;
using TeachReach.TeachReach.Application.Services.Interfaces;
using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {

        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("teachers")]
        public async Task<List<Teacher>> GetTeachers()
        {
            
            return await _teacherService.GetAllTeachers();
        }

        [HttpGet("teacher/{id}")]
        public async Task<ActionResult<Teacher>> Get(int id)
        {
            return await _teacherService.GetById(id);
        }

        [HttpGet("teacher/email/{email}")]
        public async Task<ActionResult<Teacher>> GetByEmail(string email)
        {
            return await _teacherService.GetByEmail(email);
        }


        [HttpDelete("teacher/{id}")]
        public async Task<IActionResult> DeleteTeacher([FromRoute] int id)
        {
            try
            {
                var response = await _teacherService.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("teacher")]
        public async Task<IActionResult> CreateTeacher([FromBody] Teacher newTeacher)
        {
            try
            {
                var response = await _teacherService.Create(newTeacher);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("teacher/{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] Teacher teacher)
        {
            try
            {
                if (id != teacher.Id)
                {
                    return BadRequest("Teacher ID in the URL does not match the ID in the request body.");
                }

                var response = await _teacherService.Update(teacher);

                if (response.Success)
                {
                    return Ok("Teacher updated successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to update teacher.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
