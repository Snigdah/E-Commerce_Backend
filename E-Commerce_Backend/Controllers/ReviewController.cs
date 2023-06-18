using E_Commerce_Backend.Dto;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models.ProductModel;
using Microsoft.AspNetCore.Mvc;


namespace E_Commerce_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public ReviewController(IReviewRepository reviewRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        //Post: api/Review
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Review))]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto reviewDto)
        {
            try
            {
                //Check UserId Exists or Not
                var User = await _userRepository.GetById(reviewDto.UserId);

                if(User == null)
                {
                    return NotFound("This User ID not Exists");
                }

                //Check Product Exists or Not
                var Product = await _productRepository.GetProductById(reviewDto.ProductId);

                if (Product == null)
                {
                    return NotFound("This ProductId not Exists");
                }

                //Mapping Item
                var review = new Review();
                review.ReviewId = reviewDto.ReviewId;
                review.Comment = reviewDto.Comment;
                review.Rating = reviewDto.Rating;
                review.ProductId = reviewDto.ProductId;
                review.Product = Product;
                review.UserId = reviewDto.UserId;
                review.User = User;

                await _reviewRepository.AddReview(review);
                return Ok("Review Added Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while adding the Review item.");
            }
        }


        //Update Review
        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewDto reviewDto)
        {
            try
            {
                //Check Review Exists or Not
                var review = await _reviewRepository.GetReviewById(reviewId);
                if(review == null)
                {
                    return NotFound("This Review Id not Exists");
                }

                //Mapping Item
                review.ReviewId = review.ReviewId;
                review.Comment = reviewDto.Comment;
                review.Rating = reviewDto.Rating;
                review.ProductId = review.ProductId;
                review.Product = review.Product;
                review.UserId = review.UserId;
                review.User = review.User;

                await _reviewRepository.UpdateReview(review);
                return Ok("Review Updated");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while Updating the Review.");
            }
        }

        // DELETE: api/review/{id}
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            try
            {
                //Check Review Exists or Not
                var review = await _reviewRepository.GetReviewById(reviewId);

                if(review == null)
                {
                    return NotFound("Review Not Exists");
                }

                await _reviewRepository.DeleteReview(reviewId);
                return Ok("Review Delete Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while deleting the Review item.");
            }
        }
    }
}
