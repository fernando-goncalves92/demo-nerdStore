using Microsoft.AspNetCore.Mvc;
using NerdStore.WebApp.MVC.Models;
using System.Linq;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ExistsResponseErrors(ResponseResult response)
        {
            if (response != null && response.Errors.Messages.Any())
            {
                foreach (var message in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }

                return true;
            }

            return false;
        }
    }
}
