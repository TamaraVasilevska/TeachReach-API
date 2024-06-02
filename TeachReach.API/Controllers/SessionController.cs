using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeachReach.TeachReach.Application.RequestModels.SessionRequestModels;
using TeachReach.TeachReach.Application.Services.Implementation;
using TeachReach.TeachReach.Application.Services.Interfaces;
using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SessionController(ISessionService SessionService, IMapper mapper)
        {
            _sessionService = SessionService;
            _mapper = mapper;
        }

        [HttpGet("sessions")]
        public async Task<List<Session>> GetSessions()
        {

            return await _sessionService.GetAllSessions();
        }

        [HttpGet("session/{id}")]
        public async Task<ActionResult<Session>> Get(int id)
        {
            return await _sessionService.GetById(id);
        }

        [HttpGet("teacher/{id}/sessions")] 
        public async Task<ActionResult<List<Session>>> GetSessionsByTeacherId(int id)
        {
            try
            {
                var sessions = await _sessionService.GetAllSessionsByTeacherId(id);
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("session/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var response = await _sessionService.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("session")]
        public async Task<IActionResult> CreateSession(SessionRequestDto newSessionDto)
        {
            try
            {
                var newSession = _mapper.Map<Session>(newSessionDto);
                var response = await _sessionService.Create(newSession);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("session/{id}")]
        public async Task<IActionResult> UpdateSession(int id, [FromBody] Session session)
        {
            try
            {
                if (id != session.Id)
                {
                    return BadRequest("Session ID in the URL does not match the ID in the request body.");
                }

                var response = await _sessionService.Update(session);

                if (response.Success)
                {
                    return Ok("Session updated successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to update session.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
