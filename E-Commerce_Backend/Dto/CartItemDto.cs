using E_Commerce_Backend.Models.CartModel;
using E_Commerce_Backend.Models.ProductModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Backend.Dto
{
    public class CartItemDto
    {

        [Key]
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        // Other cart item properties

        public int ProductId { get; set; }   
    }
}

