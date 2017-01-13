using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewsInEFCoreWorkaround.Models
{
	public class Category
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public bool IsHidden { get; set; }
		public List<CategoryProduct> CategoryProduct { get; set; } = new List<CategoryProduct>();
	}
}
