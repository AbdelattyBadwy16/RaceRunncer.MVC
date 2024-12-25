using MvcMovie.Models;

namespace MvcMovie.Interfaces
{
	public interface IClubRepository
	{
		Task<ICollection<Club>> GetAll();
		Task<Club> GetByIdAsync(int id);
		Task<Club> GetByIdAsyncNoTracing(int id);
		Task<ICollection<Club>> GetClubByCity(string city);
		bool Add(Club club);
		bool Update(Club club);
		bool Delete(Club club);
		bool Save();
	}
}