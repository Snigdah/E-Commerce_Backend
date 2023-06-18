using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Backend.Models
{
    public class ProductDto
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "The product name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The product description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The product price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }
        // Other product properties
    }
}
