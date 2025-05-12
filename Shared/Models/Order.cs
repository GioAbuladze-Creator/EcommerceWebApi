using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public float TotalAmount { get; set; }
        public Status Status { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
        public DateTime CreatedAt = DateTime.UtcNow;
    }
}
