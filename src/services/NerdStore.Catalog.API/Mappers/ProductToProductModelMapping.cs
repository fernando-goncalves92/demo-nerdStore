using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Catalog.API.Entities
{
    public static class ProductToProductModelMapping
    {
        public static ProductModel Map(Product product)
        {
            return new ProductModel
            {   
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                IsActive = product.IsActive,
                Price = product.Price,
                StockAmount = product.StockAmount,
                Image = product.Image
            };
        }

        public static Product Map(ProductModel productModel)
        {
            return new Product
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                IsActive = productModel.IsActive,
                Price = productModel.Price,
                StockAmount = productModel.StockAmount,
                Image = productModel.Image
            };
        }

        public static IEnumerable<ProductModel> Map(IEnumerable<Product> products)
        {
            return products.Select(Map);
        }

        public static IEnumerable<Product> Map(IEnumerable<ProductModel> productModels)
        {
            return productModels.Select(Map);
        }
    }
}
