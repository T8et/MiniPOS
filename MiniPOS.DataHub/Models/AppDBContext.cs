using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MiniPOS.DataHub.Models;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BtHist> BtHists { get; set; }

    public virtual DbSet<BtProduct> BtProducts { get; set; }

    public virtual DbSet<BtProductCat> BtProductCats { get; set; }

    public virtual DbSet<BtSale> BtSales { get; set; }

    public virtual DbSet<BtSaleDtl> BtSaleDtls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=MINI_POS;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BtHist>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BT_HIST");

            entity.Property(e => e.AuditCode).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EntityName).HasMaxLength(50);
            entity.Property(e => e.FieldName).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<BtProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.ToTable("BT_PRODUCT");

            entity.Property(e => e.CatProductCode).HasMaxLength(20);
            entity.Property(e => e.ProductCode).HasMaxLength(20);
            entity.Property(e => e.ProductDesc).HasMaxLength(50);
            entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<BtProductCat>(entity =>
        {
            entity.HasKey(e => e.CatProductId);

            entity.ToTable("BT_PRODUCT_CAT");

            entity.Property(e => e.CatProductCode).HasMaxLength(20);
            entity.Property(e => e.CatProductDesc).HasMaxLength(100);
        });

        modelBuilder.Entity<BtSale>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BT_SALE");

            entity.Property(e => e.SaleCode).HasMaxLength(50);
            entity.Property(e => e.SaleDate).HasColumnType("datetime");
            entity.Property(e => e.SaleTotalAmt).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<BtSaleDtl>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BT_SALE_DTL");

            entity.Property(e => e.ProductCode).HasMaxLength(20);
            entity.Property(e => e.SaleCode).HasMaxLength(50);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
