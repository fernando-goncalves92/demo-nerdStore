using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Identity.API.Controllers.v1
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ICollection<string> Errors = new List<string>();

        protected bool IsValidOperation()
        {
            return !Errors.Any();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(result);                
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errorsFromModelState = modelState.Values.SelectMany(v => v.Errors);

            foreach (var error in errorsFromModelState)
            {
                AddError(error.ErrorMessage);
            }
            
            return CustomResponse();
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
