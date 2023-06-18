using E_Commerce_Backend.Models.ProductModel;

namespace E_Commerce_Backend.Models.OrderModel
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Other order item properties
        public int OrderId { get; set; }
        public Order Order { get; set; }

        
    }
}
