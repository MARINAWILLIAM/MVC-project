using Demo.DAL.Models;
using Demo.Pl.Helpers;
using Demo.Pl.Settings;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;
using System.Linq;

namespace Demo.Pl.Controllers
{
	
	public class AccountController : Controller
	{
        private readonly ISmsServices smsServices;
        private readonly IEmailSettings _emailSettings;
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(ISmsServices smsServices,IEmailSettings emailSettings,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInManager)
		{
            this.smsServices = smsServices;
            _emailSettings = emailSettings;
            _userManager = userManager;
			_signInManager = SignInManager;
		}
		#region Register
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVm registerVm)
		{
			if (ModelState.IsValid)//server side validation 
			{
				var User = new ApplicationUser()
				{
					FName = registerVm.FName,
					LName = registerVm.LName,
					UserName = registerVm.Email.Split("@")[0],
					Email = registerVm.Email,
					IsAgree = registerVm.IsAgree,


				};
				var result = await _userManager.CreateAsync(User, registerVm.Password);
				if (result.Succeeded)

					return RedirectToAction(nameof(Login));

				foreach (var error in result.Errors)

					ModelState.AddModelError(string.Empty, error.Description);

			}
			return View(registerVm);





		}

		#endregion
		#region LogIn
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async  Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
       if(ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
			if(user is not null)
				{
					var flag=await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
						if (result.Succeeded)
							return RedirectToAction("Index", "Home");
					}
					ModelState.AddModelError(string.Empty, "password is not valid");

				}
				ModelState.AddModelError(string.Empty, "email is not valid");
			
			
			
			
			}

	   return View(loginViewModel);






		}







		#endregion
		#region LogOut
		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		#endregion
		#region Forget Password
		public IActionResult ForgetPassword()
		{
			return View();
		}

        //[HttpPost]
        //public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);
        //        if (user is not null)
        //        {
        //            var token = await _userManager.GeneratePasswordResetTokenAsync(user); // Valid Just Only One Time Per User
        //            var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
        //            var email = new Email()
        //            {
        //                Subject = "Reset Password",
        //                To = model.Email,
        //                Body = passwordResetLink
        //            };
        //            emailSettings.SendEmail(email);
        //            return RedirectToAction(nameof(CheckYourInbox));
        //        }
        //        ModelState.AddModelError(string.Empty, "Email is not Existed");
        //    }
        //    return View(model);
        //}
         [HttpPost]

		public async Task<IActionResult> SendEmail(ForgetPassword model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
               if (user is not null)
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var PassWordResetLink=Url.Action("ResetPassword","Account",new {email=user.Email,token},Request.Scheme);
					var email = new Email()
					{
						Subject = "Reset Password",
						To = model.Email,
						Body = PassWordResetLink
					};
                    _emailSettings.SendEmail(email);
                    //Emailsettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "Invalid Email");
			}
			
			return RedirectToAction("ForgetPassword", "Account");
		}
		public IActionResult CheckYourInbox()
		{
			return View();
		}
		#endregion
		#region Reset Password
		public IActionResult ResetPassword(string email ,string token)
		{
			TempData["email"]=email; TempData["token"]=token;	
			return View();
		}
		public async Task<IActionResult> SendSms(ForgetPassword model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(user); // Valid Just Only One Time Per User
					var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
					var sms = new SmsMessage()
					{
						phoneNumber = user.PhoneNumber,
						Body = passwordResetLink
					};
					smsServices.Send(sms);
					return Ok("Check your Phone");
				}
				ModelState.AddModelError(string.Empty, "Email is not Existed");
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
		{

			if (ModelState.IsValid)
			{
				string email = TempData["email"] as string;
				string token = TempData["token"] as string;
				var user = await _userManager.FindByEmailAsync(email);
				var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
				if (result.Succeeded)
				{ 
					return RedirectToAction(nameof(Login));
			      }
				      foreach(var error in result.Errors)
				
					ModelState.AddModelError(string.Empty, error.Description);
				

			
			
			
			
			
			
			
			
			
			
			}

		return View();















		}
		//public IActionResult GoogleLogin()
		//{
		//	var prop = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
		//	return Challenge(prop, GoogleDefaults.AuthenticationScheme);
		//}

		//public async Task<IActionResult> GoogleResponse()
		//{
		//	var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
		//	var Claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
		//	{
		//		claim.Issuer,
		//		claim.OriginalIssuer,
		//		claim.Type,
		//		claim.Value
		//	});
		//	return RedirectToAction("Index", "Home");
			
		//}

























		#endregion
	}
}
