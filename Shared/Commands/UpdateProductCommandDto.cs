using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.Commands
{
    public class UpdateProductCommandDto
    {
        public int Id { get; set; }
        [Length(2, 50), RegularExpression("^(?:[A-Za-z]+)$")]
        public string? Name { get; set; }
        [Length(2, 300), RegularExpression("^[A-Za-z\\s.,!?()-]+$")]
        public string? Description { get; set; }
        [RegularExpression("^\\d+(\\.\\d{1,2})?$\r\n")]
        public string? Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be 0 or greater.")]
        public int? Stock { get; set; }
    }
}
