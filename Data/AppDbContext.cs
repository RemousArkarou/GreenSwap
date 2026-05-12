using GreenSwap.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

namespace GreenSwap.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Moderation> Moderations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
               "server=localhost;user=root;password=;database=csd_GreenSwap",
               ServerVersion.Parse("8.0.30"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------------------- USER SEED ----------------------

            modelBuilder.Entity<User>().HasData(

                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@greenswap.nl",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin",
                    GreenCredit = 500,
                    RegistrationDate = new DateTime(2025, 1, 1)
                },

                new User
                {
                    Id = 2,
                    Name = "Lars",
                    Email = "lars@greenswap.nl",
                    Password = BCrypt.Net.BCrypt.HashPassword("test123"),
                    Role = "User",
                    GreenCredit = 150,
                    RegistrationDate = new DateTime(2025, 1, 5)
                },

                new User
                {
                    Id = 3,
                    Name = "Emma",
                    Email = "emma@greenswap.nl",
                    Password = BCrypt.Net.BCrypt.HashPassword("emma123"),
                    Role = "Moderator",
                    GreenCredit = 300,
                    RegistrationDate = new DateTime(2025, 1, 10)
                }
            );

            // ---------------------- PLANT SEED ----------------------

            modelBuilder.Entity<Plant>().HasData(

                new Plant
                {
                    Id = 1,
                    UserId = 2,
                    Name = "Monstera Deliciosa",
                    Description = "Grote groene kamerplant",
                    PlantType = "Kamerplant",
                    Rarity = "Normaal",
                    SoilType = "Potgrond",
                    CreditPrice = 50,
                    Status = "Beschikbaar",
                    Image = "monstera.jpg"
                },

                new Plant
                {
                    Id = 2,
                    UserId = 2,
                    Name = "Cactus",
                    Description = "Heeft weinig water nodig",
                    PlantType = "Vetplant",
                    Rarity = "Zeldzaam",
                    SoilType = "Cactusgrond",
                    CreditPrice = 30,
                    Status = "Beschikbaar",
                    Image = "cactus.jpg"
                },

                new Plant
                {
                    Id = 3,
                    UserId = 1,
                    Name = "Orchidee",
                    Description = "Bloeiende plant",
                    PlantType = "Bloemplant",
                    Rarity = "Epic",
                    SoilType = "Orchideegrond",
                    CreditPrice = 75,
                    Status = "Verkocht",
                    Image = "orchidee.jpg"
                }
            );

            // ---------------------- REVIEW SEED ----------------------

            modelBuilder.Entity<Review>().HasData(

                new Review
                {
                    Id = 1,
                    PlantId = 1,
                    BuyerId = 1,
                    Rating = 5,
                    Comment = "Mooie gezonde plant!",
                    Date = new DateTime(2025, 2, 1)
                },

                new Review
                {
                    Id = 2,
                    PlantId = 2,
                    BuyerId = 3,
                    Rating = 4,
                    Comment = "Snelle levering",
                    Date = new DateTime(2025, 2, 3)
                }
            );

            // ---------------------- TRANSACTION SEED ----------------------

            modelBuilder.Entity<Transaction>().HasData(

                new Transaction
                {
                    Id = 1,
                    PlantId = 1,
                    BuyerId = 1,
                    SellerId = 2,
                    CreditAmount = 50,
                    TransactionDate = new DateTime(2025, 2, 1),
                    Status = "Voltooid"
                },

                new Transaction
                {
                    Id = 2,
                    PlantId = 3,
                    BuyerId = 2,
                    SellerId = 1,
                    CreditAmount = 75,
                    TransactionDate = new DateTime(2025, 2, 5),
                    Status = "In behandeling"
                }
            );

            // ---------------------- MODERATION SEED ----------------------

            modelBuilder.Entity<Moderation>().HasData(

                new Moderation
                {
                    Id = 1,
                    ModeratorId = 3,
                    PlantId = 1,
                    CheckType = "Goedgekeurd",
                    Comment = "Plant voldoet aan regels",
                    Date = new DateTime(2025, 1, 20)
                },

                new Moderation
                {
                    Id = 2,
                    ModeratorId = 3,
                    PlantId = 2,
                    CheckType = "Controle",
                    Comment = "Extra controle uitgevoerd",
                    Date = new DateTime(2025, 1, 25)
                }
            );
        }
    }
}