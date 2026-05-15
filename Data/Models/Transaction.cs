using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSwap.Data.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Plant")]
        public int PlantId { get; set; }

        public int BuyerId { get; set; }

        public int SellerId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CreditAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        // Navigation properties
        public Plant Plant { get; set; }

        [InverseProperty("Purchases")]
        public User Buyer { get; set; }

        [InverseProperty("Sales")]
        public User Seller { get; set; }
    }
}