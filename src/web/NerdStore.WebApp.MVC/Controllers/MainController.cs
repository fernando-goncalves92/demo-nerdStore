using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication;
using System.Linq;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseHasErros(ResponseResult response)
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

        protected void AddError(string message)
        {
            ModelState.AddModelError(string.Empty, message);
        }

        protected bool IsValidOperation()
        {
            return ModelState.ErrorCount == 0;
        }
    }
}
