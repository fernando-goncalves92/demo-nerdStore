using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using NerdStore.WebApp.MVC.Services.Catalog;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class CatalogController : MainController
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly ICatalogService _catalogService;

        public CatalogController(ILogger<CatalogController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAll());
        }

        [HttpGet("produto-detalhe/{id:guid}")]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            return View(await _catalogService.GetById(id));
        }
    }
}
