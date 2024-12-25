using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MvcMovie.Models
{
	public class AppUser : IdentityUser
	{
		[ForeignKey("Address")]
		public int? AddressId { get; set; }
		public Address? Address { get; set; }
		public int Pace { get; set; }
		public int Mileage { get; set; }
		public ICollection<Club> Clubs { get; set; }
		public ICollection<Race> Races { get; set; }
	}
}