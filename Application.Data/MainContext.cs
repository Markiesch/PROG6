using Application.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public class MainContext(DbContextOptions<MainContext> options) : IdentityDbContext<User, IdentityRole<int>, int>(options)
{
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingDetail> BookingDetails { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedData(builder);
    }

    private static void SeedData(ModelBuilder builder)
    {
        // Seed Data
        builder.Entity<Animal>().HasData(
            // Jungle
            new Animal { Id = 1, Name = "Aap", Type = AnimalType.Jungle, Price = 25.0m, Image = "/img/animals/monkey.png" },
            new Animal { Id = 2, Name = "Olifant", Type = AnimalType.Jungle, Price = 100.0m, Image = "/img/animals/elephant.png" },
            new Animal { Id = 3, Name = "Zebra", Type = AnimalType.Jungle, Price = 50.0m, Image = "/img/animals/zebra.png" },
            new Animal { Id = 4, Name = "Leeuw", Type = AnimalType.Jungle, Price = 150.0m, Image = "/img/animals/lion.png" },

            // Boerderij
            new Animal { Id = 5, Name = "Hond", Type = AnimalType.Farm, Price = 20.0m, Image = "/img/animals/dog.png" },
            new Animal { Id = 6, Name = "Ezel", Type = AnimalType.Farm, Price = 30.0m, Image = "/img/animals/donkey.png" },
            new Animal { Id = 7, Name = "Koe", Type = AnimalType.Farm, Price = 50.0m, Image = "/img/animals/cow.png" },
            new Animal { Id = 8, Name = "Eend", Type = AnimalType.Farm, Price = 15.0m, Image = "/img/animals/duck.png" },
            new Animal { Id = 9, Name = "Kuiken", Type = AnimalType.Farm, Price = 10.0m, Image = "/img/animals/chicken.png" },
            
            // Sneeuw
            new Animal { Id = 10, Name = "Pinguïn", Type = AnimalType.Snow, Price = 80.0m, Image = "/img/animals/penguin.png" },
            new Animal { Id = 11, Name = "IJsbeer", Type = AnimalType.Snow, Price = 200.0m, Image = "/img/animals/polar-bear.png" },
            new Animal { Id = 12, Name = "Zeehond", Type = AnimalType.Snow, Price = 60.0m, Image = "/img/animals/seal.png" },

            // Woestijn
            new Animal { Id = 13, Name = "Kameel", Type = AnimalType.Desert, Price = 70.0m, Image = "/img/animals/camel.png" },
            new Animal { Id = 14, Name = "Slang", Type = AnimalType.Desert, Price = 40.0m, Image = "/img/animals/snake.png" },

            // VIP
            new Animal { Id = 15, Name = "T-Rex", Type = AnimalType.Vip, Price = 500.0m, Image = "/img/animals/t-rex.png" },
            new Animal { Id = 16, Name = "Unicorn", Type = AnimalType.Vip, Price = 1000.0m, Image = "/img/animals/unicorn.png" }
        );
    }
}
