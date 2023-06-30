using E_Commerce_Backend.Dto;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models;
using Microsoft.AspNetCore.Mvc;

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

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

        ////POST: api/product
        //[HttpPost("register")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(200, Type = typeof(User))]
        //public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
        //{
        //    try
        //    {
        //        await _userRepository.AddUser(userDto);
        //        return Ok(userDto.UserName + " Crreate Successfully");
        //    }
        //    catch (Exception)
        //    {

        //        return StatusCode(500, "An error occurred while adding the user.");
        //    }
        //}
        [HttpPost("register")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto, [FromServices] SmtpClient smtpClient)
        {
            try
            {
                // Generate a 4-digit validation code
                string validationCode = GenerateValidationCode();

                // Send the validation code to the user's email
                bool isEmailSent = await SendValidationCode(userDto.Email, validationCode, smtpClient);

                if (!isEmailSent)
                {
                    // Return an error if the email sending fails
                    return StatusCode(500, "Failed to send the validation code to the user's email.");
                }

                // Continue with the user registration process
                await _userRepository.AddUser(userDto);

                return Ok(userDto.UserName + " created successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding the user.");
            }
        }

        private string GenerateValidationCode()
        {
            // Generate a 4-digit random validation code
            Random random = new Random();
            int validationCode = random.Next(1000, 9999);
            return validationCode.ToString();
        }

        private async Task<bool> SendValidationCode(string email, string validationCode, SmtpClient smtpClient)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse("marques.mcclure78@ethereal.email"));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = "Registration Validation Code";
                message.Body = new MimeKit.TextPart("plain")
                {
                    Text = $"Your validation code is: {validationCode}"
                };

                await smtpClient.SendAsync(message);
                smtpClient.Disconnect(true);

                return true;
            }
            catch (Exception)
            {
                return false;
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
