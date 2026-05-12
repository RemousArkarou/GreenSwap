using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSwap.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Plant")]
        public int PlantId { get; set; }

        [ForeignKey("Buyer")]
        public int BuyerId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        // Navigation properties
        public Plant Plant { get; set; }

        public User Buyer { get; set; }
    }
}