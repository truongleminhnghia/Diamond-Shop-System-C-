﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DiamondShopSystem.Data.Models;

public partial class Net1804_212_1_DiamondShopSystemV3Context : DbContext
{
    public Net1804_212_1_DiamondShopSystemV3Context(DbContextOptions<Net1804_212_1_DiamondShopSystemV3Context> options)
        : base(options)
    {
    }

    public Net1804_212_1_DiamondShopSystemV3Context() { }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }


    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.UseName)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__23CAF1D8E92C0B06");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(150)
                .HasColumnName("categoryName");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CompanyName).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FoundedDate).HasColumnType("datetime");
            entity.Property(e => e.Industry).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Website).HasMaxLength(200);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerName).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Discount).HasMaxLength(50);
            entity.Property(e => e.Note).HasMaxLength(50);
            entity.Property(e => e.OrderDay).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.ShippingAddress).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Order_Customer");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.Ceritficare).HasMaxLength(50);
            entity.Property(e => e.Discount).HasMaxLength(50);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UnitPrice)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.WarrantlyCardId)
                .HasMaxLength(50)
                .HasColumnName("WarrantlyCardID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderDetail_products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__2D10D16AE163A10F");

            entity.ToTable("products");

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .HasColumnName("brand");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.Diamond)
                .HasMaxLength(100)
                .HasColumnName("diamond");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(200)
                .HasColumnName("productName");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__products__catego__31EC6D26");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}