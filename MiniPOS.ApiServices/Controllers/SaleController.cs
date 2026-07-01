using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniPOS.DataHub.Models;
using MiniPOS.Services.Common;

namespace MiniPOS.ApiServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        AppDBContext db;
        SaleServices services;
        public SaleController()
        {
            db = new AppDBContext();
            services = new SaleServices();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await services.GetAll();
            return Ok(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string code)
        {
            var response = await services.GetById(code);
            if (response is null) return BadRequest("Data Not exists");
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduct(BtSale sale)
        {
            try
            {
                await services.Create(sale);
                return Ok(sale);
            }
            catch
            {
                return BadRequest(sale);
            }
        }
    }
}
