using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Backend.Dto
{
    public class ReviewDto
    {
        [Key]
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        // Other review properties

        public int ProductId { get; set; }

        public int UserId { get; set; }
    }
}
