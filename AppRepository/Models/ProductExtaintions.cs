using Models;
using WebAppMVC;

namespace AppRepository.Models
{
    public static class ProductExtaintions
    {
        public static Product ToModel(this AddProductViewModel product)
        {
            var productAttachments = new List<ProductAttachment>();
            foreach (var item in product.ImagesURLs)
            {
                productAttachments.Add(new ProductAttachment()
                {
                    ImageUrl = item,
                    ProductID = product.ID
                });
            }
            return new Product
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                CategoryID = product.CategoryID,
                ProductAttachments = productAttachments
            };
        }
        public static Product ToModel(this ProductVeiwModel product)
        {
            var productAttachments = new List<ProductAttachment>();
            foreach (var item in product.Images)
            {
                productAttachments.Add(new ProductAttachment()
                {
                    ImageUrl = item
                });
            }
            return new Product
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                CategoryID = product.CategoryId,
                ProductAttachments = productAttachments
            };
        }
        public static ProductVeiwModel ToVeiwModel(this Product product)
        {
            return new ProductVeiwModel
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Quantity = product.Quantity,
                CategoryId = product.CategoryID,
                CategoryName = product.Category.Name,
                Images = product.ProductAttachments.Select(x => x.ImageUrl).ToList(),
            };
        }
    }
}
