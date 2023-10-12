using AppRepository;
using AppRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using WebAppMVC;

namespace WebAppMVC.Controllers
{
    public class ProductController : Controller
    {
        ProductRepo _productRepo;
        CategoryRepo _categoryRepo;
        UnitOfWork _unitOfWork;

        public ProductController(ProductRepo productRepo, CategoryRepo categoryRepo, UnitOfWork unitOfWork)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ViewData["Categories"] = _categoryRepo.GetList().Select(i => i.Name).ToList();
            List<ProductVeiwModel> products = _productRepo.List().ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            ProductVeiwModel product = _productRepo.GetProductByID(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_productRepo.GetProductByID(id));
        }

        [HttpPost]
        public IActionResult Delete(ProductVeiwModel product)
        {
            _productRepo.Delete(product.ID);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Categories"] = GetCateogries();
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                foreach (IFormFile file in product.Images)
                {
                    FileStream fileStream = new FileStream(
                        Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Images", file.FileName),
                        FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Position = 0;
                    product.ImagesURLs.Add(file.FileName);
                }



                _productRepo.Add(product);
                _unitOfWork.Commit();
                return RedirectToAction("Index");

            }
            else
            {
                ViewData["Categories"] = GetCateogries();
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categories"] = _categoryRepo.GetList().ToList();
            ProductVeiwModel product = _productRepo.GetProductByID(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductVeiwModel product)
        {
            Product _new = new Product();

            _new.ID = product.ID;
            _new.Name = product.Name;
            _new.Price = product.Price;
            _new.Quantity = product.Quantity;
            _new.Description = product.Description;
            _new.CategoryID = product.CategoryId;

            _productRepo.Edit(_new, product.ID);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetCateogries()
        {
            return _categoryRepo.GetList().Select(category => new SelectListItem()
            {
                Text = category.Name,
                Value = category.ID.ToString(),
            }).ToList();
        }
    }
}
