using NerdStore.BFF.Shopping.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.BFF.Shopping.Services.Catalog
{
    public interface ICatalogService
    {
        Task<ProductDto> GetProductById(Guid id);
        //Task<IEnumerable<ProductDTO>> GetProductsByIds(IEnumerable<Guid> ids);
    }
}
