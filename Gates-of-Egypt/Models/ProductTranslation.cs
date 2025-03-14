using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gates_of_Egypt.Models
{
    public class ProductTranslation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required, MaxLength(10)]
        public string LanguageCode { get; set; } // Example: "en", "fr", "ar"

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; }
    }
}
