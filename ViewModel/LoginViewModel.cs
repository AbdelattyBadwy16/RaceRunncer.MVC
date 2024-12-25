using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MvcMovie.Data.Enum;
using MvcMovie.Models;

namespace MvcMovie.ViewModel
{
	public class LoginViewModel
	{
		[Display(Name ="Email Address")]
		[Required(ErrorMessage = "Email Address Is Required")]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}