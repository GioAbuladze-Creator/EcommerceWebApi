using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Models
{
    public class CartItem
    {
        public int CartItemId {  get; set; }
        public int CartId {  get; set; }
        public int Quantity {  get; set; }
        public int ProductId {  get; set; }
        public virtual Product Product { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
