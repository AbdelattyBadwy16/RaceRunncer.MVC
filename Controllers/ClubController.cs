using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcMovie.Data;
using MvcMovie.Interfaces;
using MvcMovie.Models;
using MvcMovie.ViewModel;

namespace MvcMovie.Controllers
{
	public class ClubController : Controller
	{
		private readonly IClubRepository _clubRepository;
		private readonly IPhotoService _photoService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ClubController(IClubRepository clubRepository , IPhotoService photoService ,IHttpContextAccessor httpContextAccessor)
		{
			_clubRepository = clubRepository;
			_photoService = photoService;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<IActionResult> Index()
		{
			var clubs = await _clubRepository.GetAll();
			return View(clubs);
		}
		
		public async Task<IActionResult> Detail(int id)
		{
			Club club = await _clubRepository.GetByIdAsync(id);
			return View(club);
		}
		
		public IActionResult Create()
		{
			var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
			var createClubViewModel = new CreateClubViewModel() { AppUserId = curUserId };
			return View(createClubViewModel);
		}
		
		public async Task<IActionResult> Edit(int id)
		{
			var club = await _clubRepository.GetByIdAsync(id);
			if (club == null) return View("Error");
			var newClub = new EditClubViewModel
			{
				Title = club.Title,
				Description = club.Description,
				Address = club.Address,
				AddressId = club.AddressId,
				URL = club.Image,
				ClubCategory = club.ClubCategory,
			};
			
			return View(newClub);
		}
		
		[HttpPost]
		public async Task<IActionResult> Edit(int id, EditClubViewModel club)
		{
			if(ModelState.IsValid)
			{
				var curClub = await _clubRepository.GetByIdAsyncNoTracing(id);
				if (curClub == null) return View();
				try
				{
					await _photoService.DeletePhotoAsync(curClub.Image);
				}catch(Exception ex)
				{
					ModelState.AddModelError("", "Could not delete photo.");
					return View(club);
				}

				var photoResult = await _photoService.AddPhotoAsync(club.Image);
				var newclub = new Club
				{
					Id = id,
					Title = club.Title,
					Description = club.Description,
					Image = photoResult.Url.ToString(),
					AddressId = club.AddressId,
					Address = club.Address,
				};
				_clubRepository.Update(newclub);

				return RedirectToAction("Index");
			}
			return View("Edit",club);
		}
		
		
		[HttpPost]
		public async Task<IActionResult> Create(CreateClubViewModel club)
		{  
			if(ModelState.IsValid)
			{
				var result = await _photoService.AddPhotoAsync(club.Image);
				var newClub = new Club
				{
					Title = club.Title,
					Description = club.Description,
					Image = result.Url.ToString(),
					AppUserId = club.AppUserId,
					Address = new Address
					{
						City = club.Address.City,
						State= club.Address.State,
						Streat = club.Address.Streat,
					}
				};
				_clubRepository.Add(newClub);
				return RedirectToAction("Index");
			}
			ModelState.AddModelError("", "Photo upload failed");
			return View(club);
		}
	}
}