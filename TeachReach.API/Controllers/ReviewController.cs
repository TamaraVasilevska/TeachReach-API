using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeachReach.TeachReach.Application.RequestModels.ReviewRequestModels;
using TeachReach.TeachReach.Application.Services.Implementation;
using TeachReach.TeachReach.Application.Services.Interfaces;
using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [HttpGet("reviews")]
        public async Task<List<Review>> GetReviews()
        {

            return await _reviewService.GetAllReviews();
        }

        [HttpGet("review/{id}")]
        public async Task<ActionResult<Review>> Get(int id)
        {
            return await _reviewService.GetById(id);
        }

        [HttpDelete("review/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var response = await _reviewService.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("review")]
        public async Task<IActionResult> CreateReview(ReviewRequestDto newReviewDto)
        {
            try
            {
                var newReview = _mapper.Map<Review>(newReviewDto);
                var response = await _reviewService.Create(newReview);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("review/{id}")]
        public async Task<IActionResult> Updatereview(int id, [FromBody] Review review)
        {
            try
            {
                if (id != review.Id)
                {
                    return BadRequest("Review ID in the URL does not match the ID in the request body.");
                }

                var response = await _reviewService.Update(review);

                if (response.Success)
                {
                    return Ok("Review updated successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to update review.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
