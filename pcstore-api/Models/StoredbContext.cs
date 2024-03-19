using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pcstore_api.Models;

public partial class StoredbContext : DbContext
{
    public StoredbContext()
    {
    }

    public StoredbContext(DbContextOptions<StoredbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Computer> Computers { get; set; }

    public virtual DbSet<GraphicsCard> GraphicsCards { get; set; }

    public virtual DbSet<Memory> Memories { get; set; }

    public virtual DbSet<Processor> Processors { get; set; }

    public virtual DbSet<Psu> Psus { get; set; }

    public virtual DbSet<Storage> Storages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.Name)
                .HasColumnType("varchar(200)")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Computer>(entity =>
        {
            entity.ToTable("Computer");

            entity.Property(e => e.ComputerId).HasColumnName("computer_id");
            entity.Property(e => e.Description)
                .HasColumnType("nvarchar(500)")
                .HasColumnName("description");
            entity.Property(e => e.GraphicsCardId).HasColumnName("graphics_card_id");
            entity.Property(e => e.MemoryId).HasColumnName("memory_id");
            entity.Property(e => e.PortConfig)
                .HasColumnType("JSON")
                .HasColumnName("port_config");
            entity.Property(e => e.ProcessorId).HasColumnName("processor_id");
            entity.Property(e => e.PsuId).HasColumnName("psu_id");
            entity.Property(e => e.StorageId).HasColumnName("storage_id");
            entity.Property(e => e.Weight)
                .HasColumnType("DECIMAL(10,2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.GraphicsCard).WithMany(p => p.Computers).HasForeignKey(d => d.GraphicsCardId);

            entity.HasOne(d => d.Memory).WithMany(p => p.Computers).HasForeignKey(d => d.MemoryId);

            entity.HasOne(d => d.Processor).WithMany(p => p.Computers).HasForeignKey(d => d.ProcessorId);

            entity.HasOne(d => d.Psu).WithMany(p => p.Computers).HasForeignKey(d => d.PsuId);

            entity.HasOne(d => d.Storage).WithMany(p => p.Computers).HasForeignKey(d => d.StorageId);
        });

        modelBuilder.Entity<GraphicsCard>(entity =>
        {
            entity.HasKey(e => e.GraphicCardId);

            entity.ToTable("GraphicsCard");

            entity.Property(e => e.GraphicCardId).HasColumnName("graphic_card_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.MemoryId).HasColumnName("memory_id");
            entity.Property(e => e.Model)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("model");

            entity.HasOne(d => d.Brand).WithMany(p => p.GraphicsCards).HasForeignKey(d => d.BrandId);

            entity.HasOne(d => d.Memory).WithMany(p => p.GraphicsCards).HasForeignKey(d => d.MemoryId);
        });

        modelBuilder.Entity<Memory>(entity =>
        {
            entity.ToTable("Memory");

            entity.Property(e => e.MemoryId).HasColumnName("memory_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Type)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("type");
            entity.Property(e => e.Unit)
                .HasColumnType("VARCHAR(5)")
                .HasColumnName("unit");

            entity.HasOne(d => d.Brand).WithMany(p => p.Memories).HasForeignKey(d => d.BrandId);
        });

        modelBuilder.Entity<Processor>(entity =>
        {
            entity.ToTable("Processor");

            entity.Property(e => e.ProcessorId).HasColumnName("processor_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.Cores).HasColumnName("cores");
            entity.Property(e => e.Model)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("model");

            entity.HasOne(d => d.Brand).WithMany(p => p.Processors).HasForeignKey(d => d.BrandId);
        });

        modelBuilder.Entity<Psu>(entity =>
        {
            entity.ToTable("PSU");

            entity.Property(e => e.PsuId).HasColumnName("psu_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.Unit)
                .HasColumnType("VARCHAR(5)")
                .HasColumnName("unit");
            entity.Property(e => e.Watts).HasColumnName("watts");

            entity.HasOne(d => d.Brand).WithMany(p => p.Psus).HasForeignKey(d => d.BrandId);
        });

        modelBuilder.Entity<Storage>(entity =>
        {
            entity.ToTable("Storage");

            entity.Property(e => e.StorageId).HasColumnName("storage_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Type)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("type");
            entity.Property(e => e.Unit)
                .HasColumnType("varchar(5)")
                .HasColumnName("unit");

            entity.HasOne(d => d.Brand).WithMany(p => p.Storages).HasForeignKey(d => d.BrandId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
