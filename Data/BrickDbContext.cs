using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Brickwell.Data;

public partial class BrickDbContext : IdentityDbContext
{
    public BrickDbContext()
    {
    }

    public BrickDbContext(DbContextOptions<BrickDbContext> options)
        : base(options)
    {
    }

    //public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    //public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<LineItem> LineItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<AspNetRole>(entity =>
    //    {
    //        entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
    //            .IsUnique()
    //            .HasFilter("([NormalizedName] IS NOT NULL)");

    //        entity.Property(e => e.Name).HasMaxLength(256);
    //        entity.Property(e => e.NormalizedName).HasMaxLength(256);
    //    });

    //    modelBuilder.Entity<AspNetRoleClaim>(entity =>
    //    {
    //        entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

    //        entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
    //    });

    //    modelBuilder.Entity<AspNetUser>(entity =>
    //    {
    //        entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

    //        entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
    //            .IsUnique()
    //            .HasFilter("([NormalizedUserName] IS NOT NULL)");

    //        entity.Property(e => e.Email).HasMaxLength(256);
    //        entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
    //        entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
    //        entity.Property(e => e.UserName).HasMaxLength(256);

    //        entity.HasMany(d => d.Roles).WithMany(p => p.Users)
    //            .UsingEntity<Dictionary<string, object>>(
    //                "AspNetUserRole",
    //                r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
    //                l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
    //                j =>
    //                {
    //                    j.HasKey("UserId", "RoleId");
    //                    j.ToTable("AspNetUserRoles");
    //                    j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
    //                });
    //    });

    //    modelBuilder.Entity<AspNetUserClaim>(entity =>
    //    {
    //        entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

    //        entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
    //    });

    //    modelBuilder.Entity<AspNetUserLogin>(entity =>
    //    {
    //        entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

    //        entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

    //        entity.Property(e => e.LoginProvider).HasMaxLength(128);
    //        entity.Property(e => e.ProviderKey).HasMaxLength(128);

    //        entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
    //    });

    //    modelBuilder.Entity<AspNetUserToken>(entity =>
    //    {
    //        entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

    //        entity.Property(e => e.LoginProvider).HasMaxLength(128);
    //        entity.Property(e => e.Name).HasMaxLength(128);

    //        entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
    //    });

    //    modelBuilder.Entity<Customer>(entity =>
    //    {
    //        entity.Property(e => e.CustomerId)
    //            .ValueGeneratedNever()
    //            .HasColumnName("customer_ID");
    //        entity.Property(e => e.Age).HasColumnName("age");
    //        entity.Property(e => e.BirthDate)
    //            .HasMaxLength(50)
    //            .HasColumnName("birth_date");
    //        entity.Property(e => e.CountryOfResidence)
    //            .HasMaxLength(50)
    //            .HasColumnName("country_of_residence");
    //        entity.Property(e => e.FirstName)
    //            .HasMaxLength(50)
    //            .HasColumnName("first_name");
    //        entity.Property(e => e.Gender)
    //            .HasMaxLength(50)
    //            .HasColumnName("gender");
    //        entity.Property(e => e.LastName)
    //            .HasMaxLength(50)
    //            .HasColumnName("last_name");
    //    });

    //    modelBuilder.Entity<LineItem>(entity =>
    //    {
    //        entity.HasKey(e => new { e.TransactionId, e.ProductId });

    //        entity.Property(e => e.TransactionId).HasColumnName("transaction_ID");
    //        entity.Property(e => e.ProductId).HasColumnName("product_ID");
    //        entity.Property(e => e.Qty).HasColumnName("qty");
    //        entity.Property(e => e.Rating).HasColumnName("rating");
    //    });

    //    modelBuilder.Entity<Order>(entity =>
    //    {
    //        entity.HasKey(e => new { e.TransactionId, e.CustomerId });

    //        entity.Property(e => e.Amount).HasColumnName("amount");
    //        entity.Property(e => e.Bank)
    //            .HasMaxLength(50)
    //            .HasColumnName("bank");
    //        entity.Property(e => e.CountryOfTransaction).HasMaxLength(50);
    //        entity.Property(e => e.Date)
    //            .HasMaxLength(50)
    //            .HasColumnName("date");
    //        entity.Property(e => e.DayOfWeek).HasMaxLength(50);
    //        entity.Property(e => e.EntryMode).HasMaxLength(50);
    //        entity.Property(e => e.Fraud).HasColumnName("fraud");
    //        entity.Property(e => e.ShippingAddress).HasMaxLength(50);
    //        entity.Property(e => e.Time).HasColumnName("time");
    //        entity.Property(e => e.TypeOfCard).HasMaxLength(50);
    //        entity.Property(e => e.TypeOfTransaction).HasMaxLength(50);
    //    });

    //    modelBuilder.Entity<Product>(entity =>
    //    {
    //        entity.Property(e => e.ProductId).ValueGeneratedNever();
    //        entity.Property(e => e.Category).HasMaxLength(50);
    //        entity.Property(e => e.PrimaryColor).HasMaxLength(50);
    //        entity.Property(e => e.SecondaryColor).HasMaxLength(50);
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
