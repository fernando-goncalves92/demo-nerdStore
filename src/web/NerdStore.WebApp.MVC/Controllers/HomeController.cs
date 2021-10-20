using Microsoft.AspNetCore.Mvc;
using NerdStore.WebApp.MVC.Models;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{code:length(3,3)}")]
        public IActionResult Error(int code)
        {
            var error = new ErrorViewModel();

            if (code == 500)
            {
                error.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                error.Title = "Ocorreu um erro!";
                error.Code = code;
            }
            else if (code == 404)
            {
                error.Message = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                error.Title = "Ops! Página não encontrada.";
                error.Code = code;
            }
            else if (code == 403)
            {
                error.Message = "Você não tem permissão para fazer isto.";
                error.Title = "Acesso Negado";
                error.Code = code;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", error);
        }
    }
}
