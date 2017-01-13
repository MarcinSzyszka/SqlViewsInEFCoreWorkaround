using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewsInEFCoreWorkaround.Models;
using ViewsInEFCoreWorkaround.Models.SqlViews;
using ViewsInEFCoreWorkaround.Services;
using Xunit;

namespace ViewsInEFCoreWorkaround.Tests
{
	public class ExecutingSqlQueryTests
	{
		[Fact]
		public void GetDataFromCategoryWithOneProductSqlView_ShouldReturnsFourObjects()
		{
			//Arrange
			var stoper = new Stopwatch();

			//Act
			stoper.Start();
			var dataForMainPage = shopService.GetDataForMainPageFromCategoryWithOneProductSqlView()
																					.Where(d => d.IsMinPhoto)
																					.GroupBy(c => c.CategoryId)
																					.Select(d => d.First())
																					.ToList();
			stoper.Stop();
			var time = stoper.Elapsed;

			//Assert 
			Assert.Equal(4, dataForMainPage.Count);
		}

		[Fact]
		public void GetDataWithStandardOrmQuery_ShouldReturnsFourObjects()
		{
			//Arrange
			var stoper = new Stopwatch();

			//Act
			stoper.Start();
			//Act
			IEnumerable<Category> categoriesQuery = shopService.GetDataForMainPageWithStandardEFQuery();
			var forMainPageQuery = from category in categoriesQuery
								   from categoryProduct in category.CategoryProduct
								   group categoryProduct.Product by category into groupedProducts
								   select new CategoryWithOneProductSqlView
								   {
									   CategoryId = groupedProducts.Key.CategoryId,
									   CategoryName = groupedProducts.Key.CategoryName,
									   ProductId = groupedProducts.First().ProductId,
									   ProductName = groupedProducts.First().ProductName,
									   ProductPrice = groupedProducts.First().ProductPrice,
									   ProductShortDescription = groupedProducts.First().ProductShortDescription,
									   UniqIdentifierForSqlView = groupedProducts.First().Photos.First().UniqIdentifierForSqlView,
									   PhotoName = groupedProducts.First().Photos.First(p => p.IsMinPhoto).PhotoName,
									   IsMinPhoto = groupedProducts.First().Photos.First(p => p.IsMinPhoto).IsMinPhoto
								   };

			var dataForMainPage = forMainPageQuery.ToList();
			stoper.Stop();
			var time = stoper.Elapsed;

			//Assert 
			Assert.Equal(4, dataForMainPage.Count());
		}

		#region CONFIGURATION
		private ShopDbService shopService;

		public ExecutingSqlQueryTests()
		{
			this.shopService = new ShopDbService("Server=.\\SQLEXPRESS;Database=SqlViewsEFCore;Integrated Security=SSPI;Trusted_Connection=True;MultipleActiveResultSets=true");
			shopService.FillDbWithFakeDataIfIsEmpty();
		}
		#endregion

	}

}
