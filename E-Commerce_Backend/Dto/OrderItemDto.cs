namespace E_Commerce_Backend.Dto
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}
