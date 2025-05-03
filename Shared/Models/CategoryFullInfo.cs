using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Models
{
    public class CategoryFullInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<int, string> Products { get; set; }
    }
}
