using AppRepository;
using AppRepository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System.Data.Entity;

namespace WebAppMVC.Controllers
{
	public class UserController : Controller
	{
		UserRepo userRepo;
		RoleRepo roleRepo;
		UnitOfWork unitOfWork;
		UserManager<User> userManager;
		public UserController(UserRepo _userRepo, RoleRepo _roleRepo, UnitOfWork _unitOfWork, UserManager<User> _userManager)
		{
			userRepo = _userRepo;
			roleRepo = _roleRepo;
			unitOfWork = _unitOfWork;
			userManager = _userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var users = userRepo.GetList().Select(u => u.ToViewModel()).ToList();
			return View(users);
		}

		[HttpGet]
		public async Task<IActionResult> EditRoles(string id)
		{
			ViewBag.Success = 0;
			User user = await userRepo.GetUserByID(id);
			ViewData["UserName"] = user.UserName;

			var userRolesList = new List<UserRole>();
			foreach (var role in GetRolesList())
			{
				var userRole = new UserRole
				{
					Name = role.Value
				};
				if (await userManager.IsInRoleAsync(user, role.Text))
					userRole.Selected = true;
				else
					userRole.Selected = false;
				userRolesList.Add(userRole);
			}
			var model = new UserRolesViewModel()
			{
				UserId = id,
				UserRoles = userRolesList
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateRoles(UserRolesViewModel userRolesViewModel)
		{
			ViewBag.Success = 0;
			var user = await userManager.FindByIdAsync(userRolesViewModel.UserId);
			ViewData["UserName"] = user.UserName;
			var roles = await userManager.GetRolesAsync(user);
			var result = await userManager.RemoveFromRolesAsync(user, roles);
			result = await userManager.AddToRolesAsync(user,
				userRolesViewModel.UserRoles.Where(r => r.Selected == true).Select(r => r.Name));

			//var result = await userRepo.AddRoleToUser(user.Id, user.Role);
			if (result.Succeeded)
			{
				ViewBag.Success = 1;
			}
			else
			{
				ViewBag.Success = 2;
			}

			return RedirectToAction("index");
		}

		private IEnumerable<SelectListItem> GetRolesList()
		{
			return roleRepo.GetList().Select(r => new SelectListItem()
			{
				Value = r.Name,
				Text = r.Name
			}).ToList();
		}

	}
}
