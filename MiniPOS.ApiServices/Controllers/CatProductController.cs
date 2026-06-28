using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MiniPOS.DataHub.Models;
using MiniPOS.Services.Common;

namespace MiniPOS.ApiServices.Controllers
{
    [Route("catProduct/[controller]")]
    [ApiController]
    public class CatProductController : ControllerBase
    {
        AppDBContext db;
        CatProductServices services;

        public CatProductController()
        {
            db = new AppDBContext();
            services = new CatProductServices();
        }

        [HttpGet("GetAll")]
        public IActionResult GetCatProducts()
        {
            var response = services.GetAll();
            return Ok(response);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(string code)
        {
            string status = "Data Not Exist";
            var respone = services.GetById(code);
            if (respone is null) return BadRequest(status);
            else return Ok(respone); 
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCatProduct(BtProductCat data)
        {
            string msg;
            msg = await services.Create(data);
            return Ok(msg);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCatProduct(string code, string code_desc)
        {
            try
            {
                string msg = await services.Update(code, code_desc);
                return Ok(msg);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCatProduct(string code)
        {
            try
            {
                string msg = await services.Delete(code);
                return Ok(msg);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
