using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.ViewModels
{
	public class ResetPasswordVM
	{
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("NewPassword", ErrorMessage = "ConfirmPassword don't match Password")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
         //public string Email { get; set; }
		//public string Token { get; set; }


	}
}
