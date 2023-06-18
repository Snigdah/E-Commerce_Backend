using E_Commerce_Backend.Dto;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models;
using E_Commerce_Backend.Models.OrderModel;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrderController(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }


        // GET: api/order/{id} 
        [HttpGet("{orderId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Order))]
        public async Task<IActionResult> GetOrderByID(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderById(orderId);

                if(order == null)
                {
                    return NotFound("Order Id not valid");
                }

                return Ok(order);

            }
            catch (Exception)
            {

                throw;
            }
        }

        //POST: api/product
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Order))]
        public async Task<IActionResult> AddOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                //Check User
                var User = await _userRepository.GetById(orderDto.UserId);
                if (User == null)
                {
                    return BadRequest("User Not Found");
                }

                await _orderRepository.AddOrder(orderDto);
                return Ok("Order Added Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while adding the Order.");
            }
        }
    }
}
