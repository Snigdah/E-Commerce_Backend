using E_Commerce_Backend.Dto;
using E_Commerce_Backend.Models;

namespace E_Commerce_Backend.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderById(int id);
        Task AddOrder(OrderDto orderDto);
    }
}
