using TeachReach.TeachReach.Application.ResponseModels;
using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Application.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllReviews();
        Task<Review> GetById(int id);
        Task<SaveResponse> Create(Review review);
        Task<SaveResponse> Update(Review review);
        Task<SaveResponse> Delete(int id);
    }
}

