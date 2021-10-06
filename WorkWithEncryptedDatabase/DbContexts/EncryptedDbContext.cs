using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace WorkWithEncryptedDatabase.DbContexts
{
    public class EncryptedDbContext : DbContext
    {
        public EncryptedDbContext([NotNullAttribute] DbContextOptions options)
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
                b.ToTable("Customers").HasKey(s => s.Id);
                b.Property(s => s.FirstName).HasColumnType("varchar(50)");
            });
        }

    }
}