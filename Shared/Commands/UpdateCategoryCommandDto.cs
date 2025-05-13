using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Commands
{
    public class UpdateCategoryCommandDto
    {
        public int Id { get; set; }
        [Length(2, 50), RegularExpression("^(?:[A-Za-z]+)$")]
        public string Name { get; set; }
    }
}
