using NerdStore.WebApp.MVC.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Services.Catalog
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(Guid id);
    }
}
