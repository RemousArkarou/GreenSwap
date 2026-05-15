using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSwap.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal GreenCredit { get; set; }

        public DateTime RegistrationDate { get; set; }
        public static User? LoggedInUser { get; set; }

        // Navigation properties
        public ICollection<Plant> Plants { get; set; } = new List<Plant>();
        public ICollection<Transaction> Purchases { get; set; } = new List<Transaction>();
        public ICollection<Transaction> Sales { get; set; } = new List<Transaction>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Moderation> Moderations { get; set; } = new List<Moderation>();
    }
}