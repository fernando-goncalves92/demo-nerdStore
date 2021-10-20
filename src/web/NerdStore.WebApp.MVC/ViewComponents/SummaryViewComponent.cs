using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.ViewComponents
{
    public class SummaryViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
