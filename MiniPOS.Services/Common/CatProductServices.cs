using Azure;
using Microsoft.EntityFrameworkCore;
using MiniPOS.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPOS.Services.Common
{
    public class CatProductServices
    {
        AppDBContext db;
        AuditLog auditLog;
        public CatProductServices()
        {
            db = new AppDBContext();
            auditLog = new AuditLog();
        }

        public List<BtProductCat> GetAll()
        {
            var response = db.BtProductCats.AsNoTracking().ToList();
            return response;
        }

        public List<BtProductCat> GetById(string code)
        {
            var response = db.BtProductCats.AsNoTracking().Where(x=>x.CatProductCode == code).ToList();
            return response;
        }

        public async Task<string> Create(BtProductCat cat)
        {
            try
            {
                await db.BtProductCats.AddAsync(cat);
                await db.SaveChangesAsync();

                bool createAudit = await auditLog.CreateAuditLog("BT_PRODUCT_CAT", "CatProductCode", "admin", "admin");

                if (createAudit)
                {
                    return "Created Successfully";
                }
                else
                {
                    return "Error in Audit";
                }      
            }
            catch
            {
                return "Error in creation";
            }
        }

        public async Task<string> Update(string code, string codedesc)
        {
            var response = db.BtProductCats.AsNoTracking().Where(x => x.CatProductCode == code).FirstOrDefault();
            if (response is null) return "Data Not Exists";

            response.CatProductCode = code;
            response.CatProductDesc = codedesc;
            db.Entry(response).State = EntityState.Modified;
            await db.SaveChangesAsync();

            bool updateaudit = await auditLog.CreateAuditLog("BT_PRODUCT_CAT", "CatProductCode", "admin", "admin");

            if (updateaudit)
            {
                return "Updated Successfully";
            }
            else return "Fail Update";
        }

        public async Task<string> Delete(string code)
        {
            var response = db.BtProductCats.AsNoTracking().Where(x => x.CatProductCode == code).FirstOrDefault();
            if (response is null) return "Data Not Exists";

            try
            {
                db.Entry(response).State = EntityState.Deleted;
                await db.SaveChangesAsync();

                bool deleteaudit = await auditLog.CreateAuditLog("BT_PRODUCT_CAT", "CatProductCode", "admin", "admin");
                if (deleteaudit) return "Deleted Successfully";
                else return "Audit Fail";
            }
            catch
            {
                return "Deleted Fail";
            }
        }
    }
}
