using ComicBookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicBookStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductSpecification> ProductSpecification { get; set; }
        public DbSet<ProductSpecificationValue> ProductSpecificationValues { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*builder.Entity<ProductSpecificationValue>()
                .HasOne(p => p.Products)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProductSpecificationValue>()
                .HasOne(p => p.ProductSpecification)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);*/

            base.OnModelCreating(builder);
        }
    }
}
