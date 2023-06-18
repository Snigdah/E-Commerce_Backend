using E_Commerce_Backend.Models.CartModel;

namespace E_Commerce_Backend.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserId(int userId);
        Task<Cart> GetCartById(int id);
        Task AddCart(Cart cart);
        Task DeleteCart(int UserId);
    }
}
