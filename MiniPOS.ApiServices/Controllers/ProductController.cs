using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniPOS.DataHub.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MiniPOS.ApiServices.Controllers
{
    [Route("product/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        AppDBContext db;

        public ProductController()
        {
            db = new AppDBContext();
        }

        [HttpGet("GetAll")]
        public IActionResult GetProducts()
        {
            var response = db.BtProducts.AsNoTracking().ToList();
            return Ok(response);
        }

        [HttpGet("GetById")]
        public IActionResult GetProductById(string code)
        {
            var response = db.BtProducts.AsNoTracking().Where(x => x.ProductCode == code).FirstOrDefault();
            if (response is null) return BadRequest();
            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult CreateProduct(BtProduct product)
        {
            string msg;            
            try
            {
                db.BtProducts.Add(product);
                db.SaveChanges();

                string auditcode = AuditCodeGeneration();

                BtHist history = new BtHist();
                history.AuditCode = auditcode;
                history.EntityName = "BT_PRODUCT";
                history.FieldName = "ProductCode";
                history.CreatedBy = "admin";
                history.CreatedOn = DateTime.Now;
                history.ModifiedBy = "admin";
                history.ModifiedOn = DateTime.Now;

                db.BtHists.Add(history);
                db.SaveChanges();

                msg = "Created Successfully";
            }
            catch
            {
                msg = "Error";
            }

            return Ok(msg);
        }

        private string AuditCodeGeneration()
        {
            string code = "";
            var response = db.BtHists
                             .AsNoTracking()
                             .OrderByDescending(x => x.AuditId)
                             .FirstOrDefault();

            if (response is null || string.IsNullOrEmpty(response.AuditCode))
            {
                code = "AU0000001";
            }
            else
            {
                var numberPart = int.Parse(response.AuditCode.Substring(2));
                numberPart++;
                code = $"AU{numberPart:D7}";
            }

            return code;
        }

    }
}
