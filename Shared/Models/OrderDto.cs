using Ecommerce.Shared.Models;

namespace Ecommerce.DAL
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public float TotalAmount { get; set; }
        public Status Status { get; set; }
        public virtual List<OrderItemDto> OrderItems { get; set; }
        public DateTime CreatedAt = DateTime.UtcNow;
    }
}