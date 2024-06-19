using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using APIwebRBA.Data.Models;

namespace APIwebRBA.Data;

public partial class APIwebRBAContext : DbContext
{
    public APIwebRBAContext()
    {
    }

    public APIwebRBAContext(DbContextOptions<APIwebRBAContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Burguer> Burguers { get; set; }

    public virtual DbSet<Promo> Promos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=APIwebRBA;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Burguer>(entity =>
        {
            entity.ToTable("Burguer");

            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Promo>(entity =>
        {
            entity.ToTable("Promo");

            entity.HasIndex(e => e.BurguerId, "IX_Promo_BurguerID");

            entity.Property(e => e.PromoId).HasColumnName("PromoID");
            entity.Property(e => e.BurguerId).HasColumnName("BurguerID");

            entity.HasOne(d => d.Burguer).WithMany(p => p.Promos).HasForeignKey(d => d.BurguerId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
