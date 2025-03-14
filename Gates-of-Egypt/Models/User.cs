using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gates_of_Egypt.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } // Consider hashing passwords

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // One-to-One relationship with Cart
        public Cart Cart { get; set; }
    }
}
