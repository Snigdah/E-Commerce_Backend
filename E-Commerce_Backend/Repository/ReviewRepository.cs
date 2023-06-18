using E_Commerce_Backend.Data;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models.ProductModel;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Backend.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }


        // Get all review by Product Id
        public async Task<IEnumerable<Review>> GetAllReview(int pageSize, int pageNumber, int productId)
        {
            var reviews = await _context.Reviews
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Where(i => i.ProductId == productId)
                .ToListAsync();

            return reviews;
        }

        //Get review By Review Id
        public async Task<Review> GetReviewById(int reviewId)
        {
            var review = await _context.Reviews
                 .Where(i => i.ReviewId == reviewId)
                .FirstOrDefaultAsync();

            return review;
        }

        // Add Review
        public async Task AddReview(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        //Update Review
        public async Task UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        // Delete Review
        public async Task DeleteReview(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);

            if(review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
