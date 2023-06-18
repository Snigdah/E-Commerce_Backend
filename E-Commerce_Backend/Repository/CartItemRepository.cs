using E_Commerce_Backend.Data;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models.CartModel;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Backend.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DataContext _context;

        public CartItemRepository(DataContext context)
        {
            _context = context;
        }

        //Get All CartItem By Cart Id
        public async Task<IEnumerable<CartItem>> GetAllCartItemByCartId(int cartId)
        {
            var cartItems = await _context.CartItems
                 //.Include(c => c.Product)   
                 .Where(ci => ci.CartId == cartId)
                 .ToListAsync();

            return cartItems;
        }

        //Get CartItem By CartItem Id
        public async Task<CartItem> GetCartItemByCardItemId(int cartItemId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(i => i.CartItemId == cartItemId);

            return cartItem;
        }

        //Add CartItem
        public async Task AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        //Update CartItem
        public async Task UpdateTaskItem(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        //Delete CartItem
        public async Task DeleteCartItem(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);

            if(cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
