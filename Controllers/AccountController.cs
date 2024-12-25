using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.ViewModel;

namespace MvcMovie.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ApplicationDbContext _context;
		public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager , ApplicationDbContext context)
		{
			_context = context;
			_userManager = userManager;	
			_signInManager = signInManager;
		}
		
		public IActionResult Login()
		{
			var response = new LoginViewModel();
			return View(response);
		}
		
		[HttpPost]
		public async Task<ActionResult> Login(LoginViewModel login)
		{
			if(ModelState.IsValid)
			{
				// user check
				var user = await _userManager.FindByEmailAsync(login.Email);
				if (user == null)
				{
					TempData["Error"] = "Wronge credentials, pleas try again.";
					return View(login);
				}
				// pass check
				var passCheck = await _userManager.CheckPasswordAsync(user,login.Password);
				if (!passCheck) 
				{
					TempData["Error"] = "Wronge credentials, pleas try again.";
					return View(login);
				}
				
				// Sign in
				var result = await _signInManager.PasswordSignInAsync(user, login.Password,false,false);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Race");
				}
			}
			return View(login); 
		}
		
		public IActionResult Register()
		{
			var response = new RegisterViewModel();
			return View(response);
		}
		
		[HttpPost]
		
		public async Task<IActionResult> Register(RegisterViewModel register)
		{
			if(ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(register.Email);
				if(user != null)
				{
					TempData["Error"] = "This Email is already used.";
					return View(register);
				}
				var newUser = new AppUser()
				{
					Email = register.Email,
					UserName = register.Email,
				};
				var response = await _userManager.CreateAsync(newUser, register.Password);
				
				if(response.Succeeded)
				{
					await _userManager.AddToRoleAsync(newUser, UserRoles.User);
				}
				return RedirectToAction("Index", "Race");
			}
			return View(register); 
		}
		
		[HttpPost]
		public async Task<IActionResult>Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Race");
		}
		
	}
}