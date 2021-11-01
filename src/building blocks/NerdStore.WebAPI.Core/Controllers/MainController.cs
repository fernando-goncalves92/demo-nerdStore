using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NerdStore.Core.Communication;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.WebAPI.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            foreach (var error in modelState.Values.SelectMany(e => e.Errors))
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ResponseResult response)
        {
            ResponseHasErros(response);

            return CustomResponse();
        }

        protected bool ResponseHasErros(ResponseResult response)
        {
            if (response == null || !response.Errors.Messages.Any())
            {
                return false;
            }

            foreach (var message in response.Errors.Messages)
            {
                AddError(message);
            }

            return true;
        }

        protected bool IsValidOperation()
        {
            return !Errors.Any();
        }

        protected void AddError(string error)
        {
            Errors.Add(error);
        }

        protected void ClearErrors()
        {
            Errors.Clear();
        }
    }
}
