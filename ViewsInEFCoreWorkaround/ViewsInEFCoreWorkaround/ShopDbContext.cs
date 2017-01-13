using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ViewsInEFCoreWorkaround.Models;
using ViewsInEFCoreWorkaround.Models.SqlViews;

namespace ViewsInEFCoreWorkaround
{
	public class ShopDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Photo> Photos { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<CategoryProduct> CategoryProducts { get; set; }
		public DbSet<CategoryWithOneProductSqlView> CategoryWithOneProductSqlView { get; set; }
		public ShopDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CategoryProduct>()
			.HasKey(t => new { t.CategoryId, t.ProductId });

			modelBuilder.Entity<CategoryProduct>()
				.HasOne(pt => pt.Category)
				.WithMany(p => p.CategoryProduct)
				.HasForeignKey(pt => pt.CategoryId);

			modelBuilder.Entity<CategoryProduct>()
				.HasOne(pt => pt.Product)
				.WithMany(t => t.CategoryProduct)
				.HasForeignKey(pt => pt.ProductId);
		}

	}
}
