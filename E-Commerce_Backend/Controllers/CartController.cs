using E_Commerce_Backend.Dto;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models.CartModel;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;

        public CartController(ICartRepository cartRepository, IUserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }


        // GET: api/cart/{id} 
        [HttpGet("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Cart))]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            try
            {
                //Check User exists
                var user = await _userRepository.GetById(userId);
                if (user == null)
                {
                    return NotFound("This is User is not exist");
                }

                //Check Cart esists
                var cart = await _cartRepository.GetCartByUserId(userId);
                if(cart == null)
                {
                    return NotFound("No Cart added for " + user.UserName);
                }

                return Ok(cart);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while retrieving the Cart.");
            }
        }


        // POST: api/cart
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Cart))]
        public async Task<IActionResult> AddCart(int userId, [FromBody] CartDto cartDto)
        {
            try
            {
                //Check User exists
                var user = await _userRepository.GetById(userId);
                if (user == null)
                {
                    return NotFound("This is User is not exist");
                }

                var cart = new Cart();
                cart.CartId = cartDto.CartId;
                cart.UserId = userId;
                cart.User = user;
                cart.CartItems = null;


                await _cartRepository.AddCart(cart);
                return Ok("Cart Added Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while adding the Cart.");
            }
        }

        // DELETE: api/cart/{id}
        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCart(int userId)
        {
            try
            {
                var user = await _userRepository.GetById(userId);
                if(user == null)
                {
                    return NotFound("This user Id not exists");
                }

                await _cartRepository.DeleteCart(userId);
                return Ok("Delete Cart Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while deleting the Cart.");
            }
        }
    }
}
