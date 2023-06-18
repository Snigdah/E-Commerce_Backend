using E_Commerce_Backend.Data;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models.CartModel;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Backend.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _context;

        public CartRepository(DataContext context)
        {
            _context = context;
        }

        //Get Cart By UserID 
        public async Task<Cart> GetCartByUserId(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);

            return cart;
        }

        //Add Cart
        public async Task AddCart(Cart cart)
        {
             _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        //Delete Cart
        public async Task DeleteCart(int UserId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == UserId);
            if(cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        //Get Cart By Cart Id
        public async Task<Cart> GetCartById(int id)
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(i => i.CartId == id);

            return cart;
        }
    }
}
