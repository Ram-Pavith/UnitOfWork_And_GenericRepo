using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UnitOfWork_GenericRepo_MAIN.Models
{
    public partial class ShoppingContext : DbContext
    {
        public ShoppingContext()
        {
        }

        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-LJOJLTJ\\SQLEXPRESS;Database=Shopping;Trusted_Connection=True;");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Eid);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .HasColumnName("phoneNumber");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasIndex(e => e.Pid, "IX_Order_PId");

                entity.Property(e => e.OrderDate).HasColumnName("orderDate");

                entity.Property(e => e.Pid).HasColumnName("PId");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Pid);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Pid);

                entity.ToTable("Product");
            });

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Eid = 1,Ename = "pavith",Age=22,Email="pavith@gmail.com",Password="1234",Dob=DateTime.Today},
                new Employee { Eid = 2, Ename = "bhavesh", Age = 22, Email = "bhavesh@gmail.com", Password = "1234", Dob = DateTime.Today }
                );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
