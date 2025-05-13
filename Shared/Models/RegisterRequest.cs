using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Models
{
    public class RegisterRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",ErrorMessage ="Invalid Password Type")]
        public string Password { get; set; }
    }
}
