using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MvcMovie.Data.Enum;
using MvcMovie.Models;

namespace MvcMovie.ViewModel
{
	public class RegisterViewModel
	{
		[Display(Name ="Email Address")]
		[Required(ErrorMessage = "Email Address Is Required.")]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Display(Name ="Confirm Password")]
		[Required(ErrorMessage = "Confirm Password Is Required.")]
		[Compare("Password", ErrorMessage = "Password not match.")]
		public string ConfirmPassword { get; set; }
	}
}