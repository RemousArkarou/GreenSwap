using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSwap.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal GreenCredit { get; set; }

        public DateTime RegistrationDate { get; set; }

        // Navigation properties
        public ICollection<Plant> Plants { get; set; }
        public ICollection<Transaction> Purchases { get; set; }
        public ICollection<Transaction> Sales { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Moderation> Moderations { get; set; }
    }
}