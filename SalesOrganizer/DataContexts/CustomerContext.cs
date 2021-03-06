﻿using Microsoft.EntityFrameworkCore;
using SalesOrganizer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.DataContexts
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                        .HasIndex(c => c.PhoneNumber)
                        .IsUnique();

            modelBuilder.Entity<Product>()
                        .HasIndex(p => p.Name)
                        .IsUnique();
        }
    }
}
