using E_Commerce_Backend.Models.ProductModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Backend.Models.CartModel
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        // Other cart item properties

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string productName { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
