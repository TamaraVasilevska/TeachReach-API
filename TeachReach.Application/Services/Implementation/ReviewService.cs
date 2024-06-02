using TeachReach.TeachReach.Application.ResponseModels;
using TeachReach.TeachReach.Application.Services.Interfaces;
using TeachReach.TeachReach.Domain.Entities;
using TeachReach.TeachReach.Infrastructure.Interfaces;

namespace TeachReach.TeachReach.Application.Services.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IGenericRepository<Review> _reviewRepository;

        public ReviewService(IGenericRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<SaveResponse> Create(Review Review)
        {
            await _reviewRepository.Create(Review);
            return new SaveResponse() { Success = true };
        }

        public async Task<SaveResponse> Delete(int id)
        {
            var ReviewToDelete = await _reviewRepository.GetById(id);
            if (ReviewToDelete == null)
            {
                throw new Exception($"Review not found");
            }
            await _reviewRepository.Delete(ReviewToDelete);
            return new SaveResponse() { Success = true };
        }

        public async Task<List<Review>> GetAllReviews()
        {
            return await _reviewRepository.GetAll();
        }

        public async Task<Review> GetById(int id)
        {
            return await _reviewRepository.GetById(id);
        }

        public async Task<SaveResponse> Update(Review Review)
        {
            var existingReview = await _reviewRepository.GetById(Review.Id);

            if (existingReview == null)
            {
                throw new Exception($"Review with ID {Review.Id} not found.");
            }

            existingReview.Comment = Review.Comment;
            existingReview.Rating = Review.Rating;

            await _reviewRepository.Update(existingReview);

            return new SaveResponse { Success = true };
        }
    }
}
