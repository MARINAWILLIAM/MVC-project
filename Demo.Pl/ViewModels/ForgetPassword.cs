using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.ViewModels
{
	public class ForgetPassword
	{ 
	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid Email")]

	public string Email { get; set; }

}
}
