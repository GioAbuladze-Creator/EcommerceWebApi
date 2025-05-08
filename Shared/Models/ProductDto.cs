namespace Ecommerce.Shared.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public Dictionary<int, string> ProductCategories { get; set; }
    }
}
