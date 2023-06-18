using E_Commerce_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Backend.Dto
{
    public class CartDto
    {
        [Key]
        public int CartId { get; set; }

        // One-to-one relationship with User
        public int UserId { get; set; }
    }
}
