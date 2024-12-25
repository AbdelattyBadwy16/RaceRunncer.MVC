using Microsoft.AspNetCore.Mvc;
using MvcMovie.Interfaces;
using MvcMovie.ViewModel;

namespace MvcMovie.Controllers
{
	public class DashbordController : Controller
	{
		private readonly IDashbordRepository _dashbordRepository;

		public DashbordController(IDashbordRepository dashbordRepository)
		{
			_dashbordRepository = dashbordRepository;
		}
		
		public async Task<IActionResult> Index()
		{
			var userRaces = await _dashbordRepository.GetAllUserRaces();
			var userClubs = await _dashbordRepository.GetAllUserClubs();
			var dashbordViewModel = new DashbordViewModel()
			{
				Races = userRaces,
				Clubs = userClubs
			};
			return View(dashbordViewModel);
		}
	}
}