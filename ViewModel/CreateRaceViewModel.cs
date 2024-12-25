using MvcMovie.Data.Enum;
using MvcMovie.Models;

namespace MvcMovie.ViewModel
{
	public class CreateRaceViewModel
	{
		public int id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Address Address{ get; set; }
		public IFormFile Image { get; set; }
		public RaceCategory RaceCategory { get; set; }
		public string AppUserId { get; set; }

	}
}