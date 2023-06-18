using E_Commerce_Backend.Models.ProductModel;

namespace E_Commerce_Backend.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReview(int pageSize, int pageNumber, int productId);
        Task<Review> GetReviewById(int reviewId);
        Task AddReview(Review review);
        Task UpdateReview(Review review);
        Task DeleteReview(int reviewId);
    }
}
