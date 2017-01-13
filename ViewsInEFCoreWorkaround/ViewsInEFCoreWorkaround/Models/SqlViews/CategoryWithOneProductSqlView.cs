using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewsInEFCoreWorkaround.Models.SqlViews
{
	public class CategoryWithOneProductSqlView
	{

		/// <summary>
		/// Uniq identifier as a trick for EF
		/// </summary>
		[Key]
		public string UniqIdentifierForSqlView { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal ProductPrice { get; set; }
		public string ProductShortDescription { get; set; }
		public string PhotoName { get; set; }
		public bool IsMinPhoto { get; set; }
	}
}
