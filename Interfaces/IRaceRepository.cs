using MvcMovie.Models;

namespace MvcMovie.Interfaces
{
	public interface IRaceRepository
	{
		Task<ICollection<Race>> GetAll();
		Task<Race> GetByIdAsync(int id);
		Task<Race> GetByIdAsyncNoTracing(int id);
		Task<ICollection<Race>> GetRaceByCity(string city);
		bool Add(Race race);
		bool Update(Race race);
		bool Delete(Race race);
		bool Save();
	}
}