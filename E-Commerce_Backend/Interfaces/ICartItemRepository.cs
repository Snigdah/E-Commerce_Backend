using E_Commerce_Backend.Models.CartModel;

namespace E_Commerce_Backend.Interfaces
{
    public interface ICartItemRepository
    {
        Task<IEnumerable<CartItem>> GetAllCartItemByCartId(int cartId);
        Task<CartItem> GetCartItemByCardItemId(int cartItemId);
        Task AddCartItem(CartItem cartItem);
        Task UpdateTaskItem(CartItem cartItem);
        Task DeleteCartItem(int cartItemId);
    }
}
