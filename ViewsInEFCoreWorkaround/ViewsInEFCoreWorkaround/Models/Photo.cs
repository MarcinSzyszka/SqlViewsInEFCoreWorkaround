using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewsInEFCoreWorkaround.Models
{
	public class Photo
	{
		public int PhotoId { get; set; }
		/// <summary>
		/// Uniq identifier as a trick for EF mapping query to objects
		/// </summary>
		public string UniqIdentifierForSqlView { get; set; } = Guid.NewGuid().ToString();
		public string PhotoName { get; set; }
		public bool IsMinPhoto { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
