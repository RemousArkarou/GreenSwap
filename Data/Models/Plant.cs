using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSwap.Data.Models
{
    public class Plant
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [MaxLength(100)]
        public string PlantType { get; set; }

        [MaxLength(100)]
        public string Rarity { get; set; }

        [MaxLength(100)]
        public string SoilType { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CreditPrice { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        [MaxLength(255)]
        public string Image { get; set; }

        // Navigation properties
        public User User { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Moderation> Moderations { get; set; }
    }
}