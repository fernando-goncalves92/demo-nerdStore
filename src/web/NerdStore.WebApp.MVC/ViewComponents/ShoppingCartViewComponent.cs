using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.WebApp.MVC.Services.ShoppingCart;

namespace NerdStore.WebApp.MVC.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IShoppingCartService _shoopingCartService;

        public ShoppingCartViewComponent(IShoppingCartService shoopingCartService)
        {
            _shoopingCartService = shoopingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _shoopingCartService.GetShoppingCart());
        }
    }
}
