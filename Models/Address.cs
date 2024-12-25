using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
	public class Address
	{
		[Key]
		public int ID { get; set; }
		public string Streat { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		
	}
}