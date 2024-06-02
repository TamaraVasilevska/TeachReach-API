using Microsoft.AspNetCore.Mvc;
using TeachReach.TeachReach.Application.Services.Interfaces;
using TeachReach.TeachReach.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeachReach.TeachReach.Application.Services.Implementation;

namespace TeachReach.TeachReach.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;


        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("students")]
        public async Task<List<Student>> GetStudents()
        {
            return await _studentService.GetAllStudents();
        }

        [HttpGet("student/{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            return await _studentService.GetById(id);
        }

        [HttpGet("student/email/{email}")]
        public async Task<ActionResult<Student>> GetByEmail(string email)
        {
            return await _studentService.GetByEmail(email);
        }


        [HttpDelete("student/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var response = await _studentService.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("student")]
        public async Task<IActionResult> CreateUser(Student newStudent)
        {
            try
            {
                var response = await _studentService.Create(newStudent);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut("student/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student student)
        {
            try
            {
                if (id != student.Id)
                {
                    return BadRequest("Student ID in the URL does not match the ID in the request body.");
                }

                var response = await _studentService.Update(student);

                if (response.Success)
                {
                    return Ok("Student updated successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to update student.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }

}
