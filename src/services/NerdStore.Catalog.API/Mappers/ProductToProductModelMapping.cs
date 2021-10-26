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
                Name = product.Name,
                Description = product.Description,
                IsActive = product.IsActive,
                Price = product.Price,
                StockAmount = product.StockAmount
            };
        }

        public static Product Map(ProductModel productModel)
        {
            return new Product
            {
                Name = productModel.Name,
                Description = productModel.Description,
                IsActive = productModel.IsActive,
                Price = productModel.Price,
                StockAmount = productModel.StockAmount
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
