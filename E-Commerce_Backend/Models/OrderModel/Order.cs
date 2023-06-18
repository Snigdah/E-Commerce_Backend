using E_Commerce_Backend.Models.OrderModel;

namespace E_Commerce_Backend.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        // Other order properties

        public int UserId { get; set; }
        public User User { get; set; }

        // Many-to-many relationship with Products (through OrderItems)
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
