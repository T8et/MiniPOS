using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniPOS.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPOS.Services.Common
{
    public class ProductServices
    {
        AppDBContext db;
        AuditLog auditLog;

        public ProductServices()
        {
            db = new AppDBContext();
            auditLog = new AuditLog();
        }

        public async Task<List<BtProduct>> GetAll()
        {
            var response = await db.BtProducts.AsNoTracking().ToListAsync();
            return response;
        } 

        public async Task<List<BtProduct>> GetById(string code)
        {
            var response = await db.BtProducts.AsNoTracking().Where(x=>x.ProductCode == code).ToListAsync();
            return response;
        }

        public async Task<string> Create(BtProduct product) 
        {
            string catcode = product.CatProductCode!;
            if (catcode is not null)
            {
                var response = await db.BtProductCats.AsNoTracking().Where(x => x.CatProductCode == catcode).FirstOrDefaultAsync();
                if (response != null)
                {
                    try
                    {
                        var response1 = await db.BtProductCats.AsNoTracking().Where(x => x.CatProductCode == product.CatProductCode).FirstOrDefaultAsync();
                        if (response1 is not null) product.CatProductCode = catcode;
                        else goto Rule;

                        await db.BtProducts.AddAsync(product);
                        await db.SaveChangesAsync();

                        bool createAudit = await auditLog.CreateAuditLog("BT_PRODUCT", "ProductCode", "admin", "admin");
                        if (createAudit) return "Created Successfully";
                        else return "Error in Audit";
                    }
                    catch
                    {
                        return "Created Error";
                    }
                }
                else goto Rule;
            }
            else
            {
                goto Rule;
            }
            Rule: return "Category Code not exist!";
        }

        public async Task<string> Update(string code, string? desc, string? price, string? catcode)
        {
            var response = await db.BtProducts.AsNoTracking().Where(x=>x.ProductCode==code).FirstOrDefaultAsync();
            if(response is not null)
            {
                if (desc is not null) response.ProductDesc = desc;
                if (price is not null) response.ProductPrice = decimal.Parse(price);
                if(catcode is not null)
                {
                    var response1 = await db.BtProductCats.AsNoTracking().Where(x=>x.CatProductCode == code).FirstOrDefaultAsync();
                    if (response1 is not null) response.CatProductCode = catcode;
                    else goto Rule;
                }
                else
                {
                    response.CatProductCode = response.CatProductCode;
                }
                try
                {
                    db.Entry(response).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    bool updateaudit = await auditLog.CreateAuditLog("BT_PRODUCT", "ProductCode", "admin", "admin");
                    if (updateaudit) return "Updated Successfully";
                    else return "Fail Update";
                }
                catch
                {
                    return "Update Error";
                }
            }
            else
            {
                return "Data not exists";
            }
            Rule: return "Category code not exists";         
        }

        public async Task<string> Delete(string code)
        {
            var response = await db.BtProducts.AsNoTracking().Where(x => x.ProductCode == code).FirstOrDefaultAsync();
            if(response is not null)
            {
                db.Entry(response).State = EntityState.Deleted;
                await db.SaveChangesAsync();

                bool deleteaudit = await auditLog.CreateAuditLog("BT_PRODUCT", "ProductCode", "admin", "admin");
                if (deleteaudit) return "Deleted Successfully";
                else return "Audit Fail";
            }
            else
            {
                return "Data Not Exists";
            }
        }
    }
}
