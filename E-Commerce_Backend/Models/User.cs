using E_Commerce_Backend.Models.CartModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Backend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The product UserName is required.")]
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [Required(ErrorMessage = "The product Email is required.")]
        public string Email { get; set; }

        // One-to-many relationship with Orders
        public ICollection<Order> Orders { get; set; }
        // One-to-one relationship with Cart
        public Cart Cart { get; set; }
    }
}
