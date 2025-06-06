﻿namespace Ecommerce.Shared.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public virtual ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}
