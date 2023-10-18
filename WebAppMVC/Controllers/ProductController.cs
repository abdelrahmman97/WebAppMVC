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
            ViewData["Categories"] = GetCateogries();
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
            ViewData["Categories"] = GetCateogries();
            ProductVeiwModel product = _productRepo.GetProductByID(id);
            AddProductViewModel edit = new AddProductViewModel();
            edit.ID = product.ID;
            edit.Name = product.Name;
            edit.Description = product.Description;
            edit.Price = product.Price;
            edit.Quantity = product.Quantity;
            edit.CategoryID = product.CategoryId;
            edit.ImagesURLs = product.Images;

            return View(edit);
        }

        [HttpPost]
        public IActionResult Edit(AddProductViewModel product)
        {
            Product _new = new Product();

            _new.ID = product.ID;
            _new.Name = product.Name;
            _new.Price = product.Price;
            _new.Quantity = product.Quantity;
            _new.Description = product.Description;
            _new.CategoryID = product.CategoryID;

            _productRepo.Edit(_new, product.ID);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public IActionResult Search
            (
            int ID = 0,
            string Name = null,
            string CategoryName = null,
            int CategoryID = 0,
            double Price = 0,
            string OrderBy = "Price",
            bool IsAscending = false,
            int PageSize = 6,
            int PageIndex = 1
            )
        {
            ViewData["Categories"] = GetCateogries();
            var res = _productRepo.Search(Name, CategoryName, CategoryID, ID, Price, OrderBy, IsAscending, PageSize, PageIndex);
            return View(res);
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
