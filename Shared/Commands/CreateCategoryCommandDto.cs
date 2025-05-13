using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.Commands
{
    public class CreateCategoryCommandDto
    {
        [Length(2, 50), RegularExpression("^(?:[A-Za-z]+)$")]
        public string Name { get; set; }
    }
}