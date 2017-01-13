using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewsInEFCoreWorkaround.Models
{
	public class Product
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal ProductPrice { get; set; }
		public string ProductShortDescription { get; set; }
		public string FullDescription { get; set; }
		public bool IsHidden { get; set; }
		public int QuantityInWarehouse { get; set; }
		public List<Photo> Photos { get; set; } = new List<Photo>();
		public List<CategoryProduct> CategoryProduct { get; set; } = new List<CategoryProduct>();
	}
}
