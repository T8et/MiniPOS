using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MiniPOS.DataHub.Models;

namespace MiniPOS.ApiServices.Controllers
{
    [Route("catProduct/[controller]")]
    [ApiController]
    public class CatProductController : ControllerBase
    {
        AppDBContext db;

        public CatProductController()
        {
            db = new AppDBContext();
        }

        [HttpGet("GetAll")]
        public IActionResult GetCatProducts()
        {
            var response = db.BtProductCats.AsNoTracking().ToList();
            return Ok(response);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(string code)
        {
            string status = "Data Not Exist";
            var respone = db.BtProductCats.AsNoTracking().Where(x => x.CatProductCode == code).FirstOrDefault();
            if (respone is null) return BadRequest(status);
            else return Ok(respone); 
        }

        [HttpPost("Create")]
        public IActionResult CreateCatProduct(BtProductCat data)
        {
            string msg;
            try
            {
                db.BtProductCats.Add(data);
                db.SaveChanges();
                msg = "Created Successfully";
            }
            catch
            {
                msg = "Error";
            }

            return Ok(msg);
        }

        [HttpPut("Update")]
        public IActionResult UpdateCatProduct(string code, string code_desc)
        {
            string status = "Update Fail";
            var response = db.BtProductCats.AsNoTracking().Where(x=>x.CatProductCode == code).FirstOrDefault();

            if (response is null) return BadRequest();

            if (code != null) response.CatProductCode = code;
            if (code_desc != null) response.CatProductDesc = code_desc;

            try
            {
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(response);
            }
            catch
            {
                return BadRequest(status);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteCatProduct(string code)
        {
            string status = "Delete Fail";
            var response = db.BtProductCats.AsNoTracking().Where(x => x.CatProductCode == code).FirstOrDefault();

            if (response is null) return BadRequest();

            try
            {
                db.Entry(response).State = EntityState.Deleted;
                db.SaveChangesAsync();
                return Ok("Delete Success");
            }
            catch
            {
                return BadRequest(status);
            }



        }
    }
}
