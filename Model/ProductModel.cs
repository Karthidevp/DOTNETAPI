using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProductModel
    {
        public class Product
        {
            public int? ProductId { get; set; }  // nullable for inserts
            public string? ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public string? Description { get; set; }
            public string? ProductImage { get; set; }
        }

        public class ProductResponse
        {
            public bool Success { get; set; }
            public string? Message { get; set; }

        }
        public class ProductDto
        {
            public int? ProductId { get; set; }  // null for insert, value for update
            public string ProductName { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public string? Description { get; set; }
            public IFormFile? ProductImage { get; set; }  // uploaded file
        }
    }
}
