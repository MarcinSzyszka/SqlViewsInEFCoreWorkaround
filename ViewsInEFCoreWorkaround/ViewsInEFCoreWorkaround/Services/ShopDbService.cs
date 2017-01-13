using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ViewsInEFCoreWorkaround.Models;
using ViewsInEFCoreWorkaround.Models.SqlViews;

namespace ViewsInEFCoreWorkaround.Services
{
	public class ShopDbService
	{
		private ShopDbContext shopContext;

		public ShopDbService(string connectionString)
		{
			var builder = new DbContextOptionsBuilder<ShopDbContext>();
			builder.UseSqlServer(connectionString);

			this.shopContext = new ShopDbContext(builder.Options);
			this.shopContext.Database.EnsureCreated();
		}

		public IEnumerable<Category> GetDataForMainPageWithStandardEFQuery()
		{
			return this.shopContext.Categories.Include(c => c.CategoryProduct).ThenInclude(cp => cp.Product).ThenInclude(p => p.Photos);

		}

		public IEnumerable<CategoryWithOneProductSqlView> GetDataForMainPageFromCategoryWithOneProductSqlView()
		{
			var data = this.shopContext.CategoryWithOneProductSqlView.FromSql("SELECT DISTINCT * FROM dbo.CategoryWithOneProductSqlView_Sql");

			return data;
		}

		public void FillDbWithFakeDataIfIsEmpty()
		{
			var defaultCategory = shopContext.Categories.FirstOrDefault();
			if (defaultCategory != null)
			{
				return;
			}

			var categoryNames = new string[] { "Clothes", "Shoes", "Backpacks", "Bags" };
			var productNames = new string[][] { new string[] {"T-shirts", "Trousers", "Jackets", "Dresses"},
												new string[] {"Summer Shoes", "Winter shoes"},
												new string[] {"Nike", "Adidas", "Reebok"},
												new string[] {"Business", "Tourist"}
											  };

			//Insert 4 categories with few products in each one category
			for (int i = 0; i < categoryNames.Length; i++)
			{

				var category = new Category();
				category.CategoryName = categoryNames[i];

				for (int j = 0; j < productNames[i].Length; j++)
				{
					var product = new Product();
					product.FullDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam vitae nulla eros. Ut ac ornare arcu. Sed quis urna felis. Maecenas rhoncus, enim a aliquam molestie, purus nunc gravida ante, non gravida velit neque quis diam. Sed ut viverra purus. Nullam a arcu gravida diam lobortis tempor et et tortor.";
					product.ProductShortDescription = "Sed volutpat rutrum justo, et finibus eros varius fringilla.";
					product.ProductName = productNames[i][j];
					product.ProductPrice = 99m;
					product.QuantityInWarehouse = 100;

					var minPhoto = new Photo();
					minPhoto.IsMinPhoto = true;
					minPhoto.PhotoName = Guid.NewGuid().ToString();

					var largePhoto = new Photo();
					largePhoto.PhotoName = Guid.NewGuid().ToString();

					product.Photos.Add(minPhoto);
					product.Photos.Add(largePhoto);

					category.CategoryProduct.Add(new CategoryProduct { Category = category, Product = product });
				}

				shopContext.Categories.Add(category);
			}

			shopContext.SaveChanges();
		}
	}
}
