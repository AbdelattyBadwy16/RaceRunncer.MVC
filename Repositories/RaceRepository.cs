using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Interfaces;
using MvcMovie.Models;

namespace MvcMovie.Repositories
{
	public class RaceRepository : IRaceRepository
	{
		private readonly ApplicationDbContext _context;

		public RaceRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public bool Add(Race race)
		{
			_context.Races.Add(race);
			return Save();
		}

		public bool Delete(Race race)
		{
			_context.Races.Remove(race);
			return Save();
		}

		public async Task<ICollection<Race>> GetAll()
		{
			return await _context.Races.ToListAsync();
		}

		public async Task<Race> GetByIdAsync(int id)
		{
			return await _context.Races.Include(r=>r.Address).FirstOrDefaultAsync(r=>r.Id == id);	
		}
		
		public async Task<Race> GetByIdAsyncNoTracing(int id)
		{
			return await _context.Races.Include(c=>c.Address).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<ICollection<Race>> GetRaceByCity(string city)
		{
			return await _context.Races.Where(r => r.Address.City.Contains(city)).ToListAsync();			
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool Update(Race race)
		{
			_context.Update(race);
			return Save();
		}
	}
}