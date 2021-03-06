﻿using System.Data.Entity;
using ProductOdata.Migrations;
using ProductOdata.Models;

namespace ProductOdata
{
    public class ProductsContext : DbContext
    {
        public ProductsContext()
            : base("ProductsContext")
        {
        }
        static ProductsContext()
        {
            //初始化时自动更新迁移到最新版本
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductsContext, Configuration>());
        }

        public DbSet<Product> Products { get; set; }
        // New code:
        public DbSet<Supplier> Suppliers { get; set; }
        // New code:
        public DbSet<ProductRating> Ratings { get; set; }

    }
}