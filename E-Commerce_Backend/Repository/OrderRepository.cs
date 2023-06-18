using E_Commerce_Backend.Data;
using E_Commerce_Backend.Dto;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models;
using E_Commerce_Backend.Models.OrderModel;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Backend.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;

        public OrderRepository(DataContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task AddOrder(OrderDto orderDto)
        {
            try
            {
                var User = await _userRepository.GetById(orderDto.UserId);

                var order = new Order();
                order.OrderId = orderDto.OrderId;
                order.OrderDate = orderDto.OrderDate;
                order.TotalAmount = orderDto.TotalAmount;
                order.UserId = orderDto.UserId;
                order.User = User;
                order.OrderItems = new List<OrderItem>();

                foreach (var orderItemDto in orderDto.OrderItems)
                {
                    var orderItem = new OrderItem();
                    orderItem.OrderItemId = orderItemDto.OrderItemId;
                    orderItem.Quantity = orderItemDto.ProductId;
                    orderItem.OrderId = orderDto.OrderId;
                    orderItem.ProductId = orderItemDto.ProductId;

                    orderItem.Order = null;
                    orderItem.Product = null;

                    // Add the orderItem to the order
                    order.OrderItems.Add(orderItem);
                }

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw new Exception("An error occurred while adding the Order.");
            }

        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(i => i.OrderId == id);

            return order;
        }
    }
}
