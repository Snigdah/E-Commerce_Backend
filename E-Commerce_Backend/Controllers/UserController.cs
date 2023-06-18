using E_Commerce_Backend.Dto;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //GET: api/users
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<IActionResult> GetAllUsers(int pageSize, int pageNumber)
        {
            try
            {
                var users = await _userRepository.GetUsers(pageSize, pageNumber);
                if (users == null)
                {
                    return NotFound("User not found");
                }

                return Ok(users);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while retrieving the users.");
            }
        }

        //GET: api/user/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<IActionResult> GetUserByID(int id)
        {
            try
            {
                var user = await _userRepository.GetById(id);
                if(user == null)
                {
                    return NotFound("User not found");
                }

                return Ok(user);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while retrieving the user.");
            }
        }

        //POST: api/product
        [HttpPost("register")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
        {
            try
            {
                await _userRepository.AddUser(userDto);
                return Ok(userDto.UserName + " Crreate Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while adding the user.");
            }
        }

        //Upadete User
        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            try
            {
                var user = await _userRepository.GetById(userId);

                if(user == null)
                {
                    return BadRequest("User not found");
                }

                user.UserId = userDto.UserId;
                user.UserName = userDto.UserName;
                user.Email = userDto.Email;
               // user.Password = null;
                user.Orders = user.Orders;
                user.Cart = user.Cart;

                await _userRepository.UpdateUser(user);
                return Ok("User Dpdate Successfilly");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while Updating the user.");
            }
        }

        // DELETE: api/product/{id}
        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var user = await _userRepository.GetById(userId);
                if(user == null)
                {
                    return BadRequest("User not found");
                }

                await _userRepository.DeleteUser(userId);
                return Ok("User delete Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }

    }
}
