using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniPOS.DataHub.Models;
using MiniPOS.Services.Common;
using System.Runtime.CompilerServices;

namespace MiniPOS.ApiServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDtlController : ControllerBase
    {
        AppDBContext db;
        SaleServices services;

        public SaleDtlController()
        {
            db = new AppDBContext();
            services = new SaleServices();
        }

        [HttpGet("GetAllDtl")]
        public async Task<IActionResult> GetAllDtl()
        {
            var response = await services.GetAllDtl();
            if(response is null) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("GetByIdDtl")]
        public async Task<IActionResult> GetByIdDtl(string code)
        {
            var response = await services.GetByIdDtl(code);
            if (response is null) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("CreateDtl")]
        public async Task<IActionResult> CreateDtl(BtSaleDtl saledtl)
        {
            var response = await services.CreateDtl(saledtl);
            if (response is null) return BadRequest();
            return Ok(response);
        }
    }
}
