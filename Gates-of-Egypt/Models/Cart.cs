using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gates_of_Egypt.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // One-to-Many relationship with CartItem
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
