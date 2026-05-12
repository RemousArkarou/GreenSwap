using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSwap.Models
{
    public class Moderation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Moderator")]
        public int ModeratorId { get; set; }

        [ForeignKey("Plant")]
        public int PlantId { get; set; }

        [MaxLength(100)]
        public string CheckType { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        // Navigation properties
        public User Moderator { get; set; }

        public Plant Plant { get; set; }
    }
}