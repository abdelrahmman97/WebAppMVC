using AppRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAppMVC.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepo _categoryRepo;
        UnitOfWork _unitOfWork;

        public CategoryController(CategoryRepo categoryRepo, UnitOfWork unitOfWork)
        {
            _categoryRepo = categoryRepo;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> categories = _categoryRepo.GetList().ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _categoryRepo.Add(category);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = _categoryRepo.GetCategoryByID(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _categoryRepo.Update(category);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_categoryRepo.GetCategoryByID(id));
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _categoryRepo.Delete(category.ID);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(_categoryRepo.GetCategoryByID(id));
        }
    }
}
