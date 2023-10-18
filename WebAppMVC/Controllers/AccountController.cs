using AppRepository;
using AppRepository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;

namespace WebAppMVC.Controllers
{
	public class AccountController : Controller
	{
		AccountRepo accountRepo;
		RoleRepo roleRepo;
		public AccountController(AccountRepo _accountRepo, RoleRepo _roleRepo)
		{
			accountRepo = _accountRepo;
			roleRepo = _roleRepo;
		}

		[HttpGet]
		public IActionResult SignUp()
		{
			ViewData["Roles"] = GetRoleList();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(UserSignUpViewModel user)
		{
			ViewData["Roles"] = GetRoleList();
			if (ModelState.IsValid)
			{
				IdentityResult result = await accountRepo.SignUp(user);
				if (result.Succeeded)
				{
					return RedirectToAction("SignIn");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
					return View();
				}
			}
			return View();
		}

		[HttpGet]
		public IActionResult SignIn(string returnUrl = "/")
		{
			var viewModel = new UserSignInViewModel() { ReturnUrl = returnUrl };
			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(UserSignInViewModel user)
		{
			if (ModelState.IsValid)
			{
				var result = await accountRepo.SignIn(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Product");
				}
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your Account is Under Reivew");
                    return View();
                }
                else
				{
					ModelState.AddModelError("Invaild", "Invaild User Name Or Password");
					return View();
				}
			}
			return View();
		}

		[HttpGet]
		public IActionResult SignOut()
		{
			accountRepo.SignOut();
			return RedirectToAction("SignIn");
		}

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {

            ViewBag.Success = false;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserChangePasswordViewModel viewModel)
        {
            viewModel.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                var result = await accountRepo.ChangePassword(viewModel);
                if (result.Succeeded)
                {
                    ViewBag.Success = true;
                }
                return View();
            }
            ViewBag.Success = false;
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            ViewBag.Success = 0;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (!string.IsNullOrEmpty(Email))
            {
                string code = await accountRepo.GetForgotPasswordCode(Email);
                if (string.IsNullOrEmpty(code))
                {
                    ViewBag.Success = 1;
                }
                else
                {
                    var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                    {
                        Credentials = new NetworkCredential("fc3adfa2d2baa9", "3e45d18a4f1af5"),
                        EnableSsl = true
                    };
                    client.Send("mail@ecommerce.com", Email, "Forget Password Verification", $"Your Code is {code}");
                    ViewBag.Success = 2;
                }
                return View();
            }
            ViewBag.Success = 1;
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPasswordVerification()
        {
            ViewBag.Success = 0;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordVerification(UserForgotPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accountRepo.ForgotPassword(viewModel);
                if (!result.Succeeded)
                {
                    ViewBag.Success = 1;
                }
                else
                {
                    ViewBag.Success = 2;
                }
                return View();
            }
            ViewBag.Success = 1;
            return View();
        }

        private IEnumerable<SelectListItem> GetRoleList()
		{
			return roleRepo.GetList().Select(r => new SelectListItem()
			{
				Value = r.Name,
				Text = r.Name
			}).ToList();
		}
	}
}
