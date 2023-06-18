using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Backend.Models.CartModel
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        // One-to-one relationship with User
        public int UserId { get; set; }
        public User User { get; set; }

        // One-to-many relationship with CartItems
        public ICollection<CartItem> CartItems { get; set; }
    }
}
