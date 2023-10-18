using AppRepository.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Models;
using System.ComponentModel.DataAnnotations;
using WebAppMVC;

namespace AppRepository
{
    public class ProductRepo : MainRepo<Product>
    {
        public ProductRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext) { }

        public List<ProductVeiwModel> List()
        {
            return GetList().Include(c => c.Category).Include(a => a.ProductAttachments).Select(i => i.ToVeiwModel()).ToList();
        }

        public ProductVeiwModel GetProductByID(int id)
        {
            return List().Where(i => i.ID == id).FirstOrDefault();
        }

        public void Edit(Product product, int id)
        {
            var old = GetList().Where(i => i.ID == id).FirstOrDefault();
            old.Name = product.Name;
            old.Description = product.Description;
            old.Price = product.Price;
            old.Quantity = product.Quantity;
            old.CategoryID = product.CategoryID;

            Update(old);
        }

        public void Delete(int id)
        {
            var product = GetList().Where(p => p.ID == id).FirstOrDefault();
            Delete(product);
        }

        public void Add(AddProductViewModel product)
        {
            var temp = product.ToModel();
            base.Add(temp);
        }

        public PaginationViewModel<List<ProductVeiwModel>> Search
            (
            string? Name = null,
            string? CategoryName = null,
            int CategoryID = 0,
            int ProductID = 0,
            double Price = 0,
            string OrderBy = "ID",
            bool IsAscending = false,
            int PageSize = 6,
            int PageIndex = 1
            )
        {
            var filter = PredicateBuilder.New<Product>();
            var oldFilter = filter;

            if (!string.IsNullOrEmpty(Name))
                filter = filter.Or(i => i.Name.ToLower().Contains(Name.ToLower()));
            if (!string.IsNullOrEmpty(CategoryName))
                filter = filter.Or(i => i.Category.Name.ToLower().Contains(CategoryName.ToLower()));
            if (CategoryID != 0)
                filter = filter.Or(i => i.CategoryID == CategoryID);
            if (ProductID != 0)
                filter = filter.Or(i => i.ID == ProductID);
            if (Price != 0)
                filter = filter.And(i => i.Price <= Price);
            if (oldFilter == filter)
                filter = null;

            var count = (filter != null) ? GetList().Where(filter).Count() : base.GetList().Count();
            var result = GetList(filter, OrderBy, IsAscending, PageSize, PageIndex);
            
            return new PaginationViewModel<List<ProductVeiwModel>>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                Count = count,
                Data = result.Include(c => c.Category).Include(a => a.ProductAttachments).Select(i => i.ToVeiwModel()).ToList()
            };
        }
    }
}
