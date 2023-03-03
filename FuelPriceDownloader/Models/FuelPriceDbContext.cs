using Microsoft.EntityFrameworkCore;

namespace FuelPriceDownloader.Models;

public partial class FuelPriceDbContext : DbContext
{
    public FuelPriceDbContext()
    {
    }

    public FuelPriceDbContext(DbContextOptions<FuelPriceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FuelPrice> FuelPrices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FuelPrice>(entity =>
        {
            entity.HasKey(e => e.RecordDate);

            entity.Property(e => e.RecordDate).HasColumnType("date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
