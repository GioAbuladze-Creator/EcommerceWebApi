using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Models
{
    public class Cart
    {
        public int CartId {  get; set; }
        public int UserId {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual List<CartItem> CartItems { get; set; }
    }
}
