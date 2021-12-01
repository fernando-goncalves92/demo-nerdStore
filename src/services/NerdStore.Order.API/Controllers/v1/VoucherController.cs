using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Order.API.Application.Queries.Voucher;
using NerdStore.WebAPI.Core.Controllers;
using System.Threading.Tasks;

namespace NerdStore.Order.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VoucherController : MainController
    {
        private readonly IVoucherQuery _voucherQuery;

        public VoucherController(IVoucherQuery voucherQuery)
        {
            _voucherQuery = voucherQuery;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> ObterPorCodigo(string code)
        {
            if (string.IsNullOrEmpty(code)) 
                return NotFound();

            var voucher = await _voucherQuery.GetVoucherByCode(code);

            return voucher == null 
                ? NotFound() 
                : CustomResponse(voucher);
        }
    }
}
