using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.API.Data;
using NerdStore.Catalog.API.Entities;
using NerdStore.WebAPI.Core.Attributes;
using System;
using System.Threading.Tasks;

namespace NerdStore.Catalog.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts() => Ok(ProductToProductModelMapping.Map(await _productRepository.GetAll()));

        [ClaimsAuthorize("Catalogo", "Ler")]
        [HttpGet("products/{id:guid}")]
        public async Task<IActionResult> GetProdutById(Guid id) => Ok(ProductToProductModelMapping.Map(await _productRepository.GetById(id)));
    }
}
