using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gates_of_Egypt.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // One-to-Many relationship with ProductTranslation
        public List<ProductTranslation> Translations { get; set; } = new List<ProductTranslation>();
    }
}
