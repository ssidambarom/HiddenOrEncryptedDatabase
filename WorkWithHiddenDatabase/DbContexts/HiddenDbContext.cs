using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace WorkWithHiddenDatabase.DbContexts
{
    public class HiddenDbContext : DbContext
    {
        public HiddenDbContext([NotNullAttribute] DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            base.OnModelCreating(builder);

            builder.Entity<Customer>(b =>
            {
                b.ToTable("Customers");
                b.HasKey(_ => _.Id);
            });
        }

    }
}