using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.WebApp.MVC.Services.ShoppingBff;

namespace NerdStore.WebApp.MVC.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IShoppingBffService _shoopingBffService;

        public ShoppingCartViewComponent(IShoppingBffService shoopingBffService)
        {
            _shoopingBffService = shoopingBffService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _shoopingBffService.GetShoppingCartAmount());
        }
    }
}
