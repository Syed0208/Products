﻿namespace Application.Products.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockAvailable { get; set; }
    }
}
