using System;
using System.Collections.Generic;
using DataStructureAndAlgorithms.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataStructureAndAlgorithms.Data;

public partial class ShopContext : DbContext
{
    public ShopContext()
    {
    }
    
    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }

    private string? getSecretString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddUserSecrets<ShopContext>()
            .Build();

        string? secret = config.GetConnectionString("DataStructureAndAlgorithms");
        return secret;
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer(getSecretString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B820CC3286");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrdersId).HasName("PK__Orders__630B995641DBF28A");

            entity.Property(e => e.OrdersId).HasColumnName("OrdersID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Mount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__Mount__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
