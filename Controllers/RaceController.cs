using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Interfaces;
using MvcMovie.Models;
using MvcMovie.ViewModel;

namespace MvcMovie.Controllers
{
	public class RaceController : Controller
	{
		private readonly IRaceRepository _raceRepository;
		private readonly IPhotoService _photoService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public RaceController(IRaceRepository raceRepository , IPhotoService photoService,IHttpContextAccessor httpContextAccessor)
		{
			_raceRepository = raceRepository;
			_photoService = photoService;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<IActionResult> Index()
		{
			var races = await _raceRepository.GetAll();
			return View(races);
		}
		
		public async Task<IActionResult> Detail(int id)
		{
			Race race= await _raceRepository.GetByIdAsync(id);
			return View(race);
		}
		public async Task<IActionResult> Create()
		{
		    var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
			var createRaceViewModel = new CreateRaceViewModel() { AppUserId = curUserId };
			return View(createRaceViewModel);
		}
		
		[HttpPost]
		public async Task<IActionResult> Create(CreateRaceViewModel race)
		{  
			if(ModelState.IsValid)
			{
				var result = await _photoService.AddPhotoAsync(race.Image);
				var newClub = new Race
				{
					Title = race.Title,
					Description = race.Description,
					Image = result.Url.ToString(),
					AppUserId = race.AppUserId,
					Address = new Address
					{
						City = race.Address.City,
						State= race.Address.State,
						Streat = race.Address.Streat,
					}
				};
				_raceRepository.Add(newClub);
				return RedirectToAction("Index");
			}
			ModelState.AddModelError("", "Photo upload failed");
			return View(race);
		}
		
		public async Task<IActionResult> Edit(int id)
		{
			var race = await _raceRepository.GetByIdAsync(id);
			if (race == null) return View("Error");
			var newRace = new EditRaceViewModel
			{
				Title = race.Title,
				Description = race.Description,
				Address = race.Address,
				AddressId = race.AddressId,
				URL = race.Image,
				RaceCategory = race.RaceCategory,
			};
			
			return View(newRace);
		}
		
		[HttpPost]
		public async Task<IActionResult> Edit(int id, EditRaceViewModel race)
		{
			if(ModelState.IsValid)
			{
				var curClub = await _raceRepository.GetByIdAsyncNoTracing(id);
				if (curClub == null) return View();
				try
				{
					await _photoService.DeletePhotoAsync(curClub.Image);
				}catch(Exception ex)
				{
					ModelState.AddModelError("", "Could not delete photo.");
					return View(race);
				}

				var photoResult = await _photoService.AddPhotoAsync(race.Image);
				var newRace = new Race
				{
					Id = id,
					Title = race.Title,
					Description = race.Description,
					Image = photoResult.Url.ToString(),
					AddressId = race.AddressId,
					Address = race.Address,
				};
				_raceRepository.Update(newRace);

				return RedirectToAction("Index");
			}
			return View("Edit",race);
		}
	}
}