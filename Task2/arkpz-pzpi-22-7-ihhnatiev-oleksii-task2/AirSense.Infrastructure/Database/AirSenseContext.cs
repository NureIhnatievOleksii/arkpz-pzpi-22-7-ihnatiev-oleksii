using AirSense.Domain.AirQualityAggregate;
using AirSense.Domain.AirQualityHistoryAggregate;
using AirSense.Domain.LocationAggregate;
using AirSense.Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirSense.Infrastructure.Database;

public class AirSenseContext(DbContextOptions<AirSenseContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<AirQuality> AirQualitys { get; set; }
    public DbSet<AirQualityHistory> AirQualityHistorys { get; set; }
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(l => l.LocationId);
            entity.HasOne(l => l.User)
                  .WithMany(u => u.Locations) // Убедитесь, что User имеет коллекцию Locations
                  .HasForeignKey(l => l.UserId)
                  .OnDelete(DeleteBehavior.Cascade); // Устанавливаем поведение при удалении
        });


        modelBuilder.Entity<AirQuality>()
            .HasOne(a => a.Location)
            .WithMany()
            .HasForeignKey(a => a.LocationId);

        modelBuilder.Entity<AirQualityHistory>()
            .HasOne(ah => ah.User)
            .WithMany()
            .HasForeignKey(ah => ah.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Установка Restrict для избежания циклов

        modelBuilder.Entity<AirQualityHistory>()
            .HasOne(ah => ah.AirQuality)
            .WithMany()
            .HasForeignKey(ah => ah.AirQualityId)
            .OnDelete(DeleteBehavior.Restrict); // Установка Restrict для избежания циклов
    }

}
