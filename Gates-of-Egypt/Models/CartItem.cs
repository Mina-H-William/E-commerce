using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gates_of_Egypt.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CartId { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
