using MvcMovie.Data.Enum;
using MvcMovie.Models;

namespace MvcMovie.ViewModel
{
	public class EditClubViewModel
	{
		public int id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public IFormFile Image { get; set; }
		public string? URL { get; set; }
		public int? AddressId { get; set; }
		public Address? Address { get; set; }
		public ClubCategory ClubCategory { get; set; }
	}
}