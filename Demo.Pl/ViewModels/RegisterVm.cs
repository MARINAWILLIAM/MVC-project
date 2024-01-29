using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Demo.Pl.ViewModels
{
	public class RegisterVm



	{
		

		public string FName { get; set; }


		public string LName { get; set; }



		[Required(ErrorMessage ="Email is required")]
		[EmailAddress(ErrorMessage ="Invalid Email")]

		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[Required(ErrorMessage = "ConfirmPassword is required")]
		[Compare("Password", ErrorMessage = "ConfirmPassword don't match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
		public bool IsAgree { get; set; }






	}
}
