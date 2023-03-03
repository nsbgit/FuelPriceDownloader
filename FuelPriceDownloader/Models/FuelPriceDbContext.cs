using System;
using System.Collections.Generic;
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
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//    => optionsBuilder.UseSqlServer("Server=.;Database=FuelPricesDb;Trusted_Connection=True;TrustServerCertificate=true;");

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
