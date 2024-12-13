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
        
        CreateRelations(builder);
        SeedData(builder);
    }

    private static void SeedData(ModelBuilder builder)
    {
        // Seed Data
        builder.Entity<Animal>().HasData(
            // Jungle
            new Animal { Id = 1, Name = "Aap", Type = AnimalType.Jungle, Price = 25.0m, Image = "/images/aap.jpg" },
            new Animal { Id = 2, Name = "Olifant", Type = AnimalType.Jungle, Price = 100.0m, Image = "/images/olifant.jpg" },
            new Animal { Id = 3, Name = "Zebra", Type = AnimalType.Jungle, Price = 50.0m, Image = "/images/zebra.jpg" },
            new Animal { Id = 4, Name = "Leeuw", Type = AnimalType.Jungle, Price = 150.0m, Image = "/images/leeuw.jpg" },

            // Boerderij
            new Animal { Id = 5, Name = "Hond", Type = AnimalType.Farm, Price = 20.0m, Image = "/images/hond.jpg" },
            new Animal { Id = 6, Name = "Ezel", Type = AnimalType.Farm, Price = 30.0m, Image = "/images/ezel.jpg" },
            new Animal { Id = 7, Name = "Koe", Type = AnimalType.Farm, Price = 50.0m, Image = "/images/koe.jpg" },
            new Animal { Id = 8, Name = "Eend", Type = AnimalType.Farm, Price = 15.0m, Image = "/images/eend.jpg" },
            new Animal { Id = 9, Name = "Kuiken", Type = AnimalType.Farm, Price = 10.0m, Image = "/images/kuiken.jpg" },

            // Sneeuw
            new Animal { Id = 10, Name = "Pinguïn", Type = AnimalType.Snow, Price = 80.0m, Image = "/images/pinguin.jpg" },
            new Animal { Id = 11, Name = "IJsbeer", Type = AnimalType.Snow, Price = 200.0m, Image = "/images/ijsbeer.jpg" },
            new Animal { Id = 12, Name = "Zeehond", Type = AnimalType.Snow, Price = 60.0m, Image = "/images/zeehond.jpg" },

            // Woestijn
            new Animal { Id = 13, Name = "Kameel", Type = AnimalType.Desert, Price = 70.0m, Image = "/images/kameel.jpg" },
            new Animal { Id = 14, Name = "Slang", Type = AnimalType.Desert, Price = 40.0m, Image = "/images/slang.jpg" },

            // VIP
            new Animal { Id = 15, Name = "T-Rex", Type = AnimalType.Vip, Price = 500.0m, Image = "/images/trex.jpg" },
            new Animal { Id = 16, Name = "Unicorn", Type = AnimalType.Vip, Price = 1000.0m, Image = "/images/unicorn.jpg" }
        );
    }

    private static void CreateRelations(ModelBuilder builder)
    {
        // Beestje Configuratie
        builder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Image).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Type).HasConversion<string>().IsRequired();
        });

        // Klant Configuratie
        builder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Infix).HasMaxLength(5);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(15);
            entity.Property(e => e.CustomerCardType).HasConversion<string>();
        });

        // Boeking Configuratie
        builder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Klant)
                .WithMany(k => k.Bookings)
                .HasForeignKey(e => e.KlantId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // BoekingDetail Configuratie
        builder.Entity<BookingDetail>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Booking)
                .WithMany(b => b.BookingDetails)
                .HasForeignKey(e => e.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Animal)
                .WithMany(b => b.BookingDetails)
                .HasForeignKey(e => e.AnimalId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
