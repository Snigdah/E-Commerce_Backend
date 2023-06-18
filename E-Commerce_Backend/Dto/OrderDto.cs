using E_Commerce_Backend.Models.OrderModel;

namespace E_Commerce_Backend.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        // Other order properties

        public int UserId { get; set; }

        // Many-to-many relationship with Products (through OrderItems)
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
