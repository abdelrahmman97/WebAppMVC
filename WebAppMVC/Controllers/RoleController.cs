using AppRepository;
using AppRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAppMVC.Controllers
{
    public class RoleController : Controller
    {
        RoleRepo roleRepo;
        UnitOfWork unitOfWork;

        public RoleController(RoleRepo _roleRepo, UnitOfWork _unitOfWork)
        {
            roleRepo = _roleRepo;
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Success = 0;
            ViewData["Roles"] = GetRolesList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                var result = await roleRepo.Create(role);
                if (result.Succeeded)
                {
                    ViewBag.Success = 1;
                }
                else
                {
                    ViewBag.Success = 2;
                }
            }
            else
            {
                ViewBag.Success = 2;
            }
            ViewData["Roles"] = GetRolesList();
            return View();
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            return View(roleRepo.GetRole(id));
        }

        [HttpPost]
        public IActionResult Delete(RoleViewModel role)
        {
            roleRepo.Delete(role.ToModel());
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        private List<RoleViewModel> GetRolesList()
        {
            return roleRepo.GetList().Select(r => new RoleViewModel()
            {
                Id = r.Id,
                Name = r.Name,
            }).ToList();
        }
    }
}
