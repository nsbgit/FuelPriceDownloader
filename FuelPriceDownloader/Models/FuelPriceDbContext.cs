using Microsoft.EntityFrameworkCore;

namespace FuelPriceDownloader.Models
{
    /// <summary>
    /// Represents a database context for the fuel price records.
    /// </summary>
    public partial class FuelPriceDbContext : DbContext
    {
        public FuelPriceDbContext()
        {
        }

        public FuelPriceDbContext(DbContextOptions<FuelPriceDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet of fuel price records.
        /// </summary>
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
}
