using AppRepository.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using WebAppMVC;

namespace AppRepository
{
    public class ProductRepo : MainRepo<Product>
    {
        public ProductRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext) { }

        public List<ProductVeiwModel> List()
        {
            return GetList().Select(i => i.ToVeiwModel()).ToList();
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
    }
}
