using MvcMovie.Models;

namespace MvcMovie.Interfaces
{
	public interface IDashbordRepository
	{
		Task<List<Race>> GetAllUserRaces();
		Task<List<Club>> GetAllUserClubs();
	}
}