using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniPOS.DataHub.Models;
using MiniPOS.Services.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MiniPOS.ApiServices.Controllers
{
    [Route("product/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        AppDBContext db;
        ProductServices services;

        public ProductController()
        {
            db = new AppDBContext();
            services = new ProductServices();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await services.GetAll();
            return Ok(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetProductById(string code)
        {
            var response = await services.GetById(code);
            if (response is null) return BadRequest();
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduct(BtProduct product)
        {
            string msg = "";            
            try
            {
                msg = await services.Create(product);
            }
            catch
            {
                return BadRequest(msg);
            }
            return Ok(msg);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(string code, string desc, double price, string catcode)
        {
            string msg = "";
            try
            {
                msg = await services.Update(code, desc, price, catcode);
                return Ok(msg);
            }
            catch
            {
                return BadRequest(msg);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string code)
        {
            string msg = "";
            try
            {
                msg = await services.Delete(code);
                return Ok(msg);
            }
            catch
            {
                return BadRequest(msg);
            }
        }

    }
}
