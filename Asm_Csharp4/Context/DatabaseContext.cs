using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Asm_Csharp4.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Asm_Csharp4.Context
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartDetails> CartDetails { get; set; }
        public virtual DbSet<Carts> Carts { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=AssignmentC4;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartDetails>(entity =>
            {
                entity.HasKey(e => new { e.IdCart, e.IdProduct })
                    .HasName("PK_CartDetails");

                entity.HasOne(d => d.IdCartNavigation)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.IdCart)
                    .HasConstraintName("FK_Cartdetails_Cart");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Cart_details_Customers");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cartdetails_Products");
            });

            modelBuilder.Entity<Carts>(entity =>
            {
                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("FK_Carts_Customer");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
