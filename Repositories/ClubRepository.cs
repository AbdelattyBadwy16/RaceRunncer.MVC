using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Interfaces;
using MvcMovie.Models;

namespace MvcMovie.Repositories
{
	public class ClubRepository : IClubRepository
	{
		private readonly ApplicationDbContext _context;

		public ClubRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public bool Add(Club club)
		{
			_context.Clubs.Add(club);
			return Save();
		}

		public bool Delete(Club club)
		{
			_context.Clubs.Remove(club);
			return Save();
		}

		public async Task<ICollection<Club>> GetAll()
		{
			return await _context.Clubs.ToListAsync();
		}

		public async Task<Club> GetByIdAsync(int id)
		{
			return await _context.Clubs.Include(c=>c.Address).FirstOrDefaultAsync(c => c.Id == id);
		}
		
		public async Task<Club> GetByIdAsyncNoTracing(int id)
		{
			return await _context.Clubs.Include(c=>c.Address).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<ICollection<Club>> GetClubByCity(string city)
		{
			return await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool Update(Club club)
		{
			_context.Update(club);
			return Save();
		}
	}
}