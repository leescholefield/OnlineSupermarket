using Microsoft.EntityFrameworkCore;
using OnlineSupermarket.Models;
using System;

namespace OnlineSupermarket.Data
{
    public class ProductContext : DbContext
    {

        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<Product> Product { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        public DbSet<PromotionItems> PromotionItems { get; set; }
    }
}
