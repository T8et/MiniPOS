using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MiniPOS.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPOS.Services.Common
{
    public class SaleServices
    {
        AppDBContext db;
        AuditLog auditLog;
        public SaleServices()
        {
            db = new AppDBContext();
            auditLog = new AuditLog();
        }

        public async Task<List<BtSale>> GetAll()
        {
            var response = await db.BtSales.AsNoTracking().ToListAsync();
            return response;
        }

        public async Task<List<BtSale>> GetById(string code)
        {
            var response = await db.BtSales.AsNoTracking().Where(x=>x.SaleCode == code).ToListAsync();
            return response;
        }

        public async Task<string> Create(BtSale sale)
        {
            try
            {
                await db.BtSales.AddAsync(sale);
                await db.SaveChangesAsync();

                bool createAudit = await auditLog.CreateAuditLog("BT_SALE", "SaleCode", "admin", "admin");
                if (createAudit) return "Created Successfully";
                else return "Error in Audit";
            }
            catch
            {
                return "Fail";
            }
        }

        public async Task<List<BtSaleDtl>> GetAllDtl()
        {
            var response = await db.BtSaleDtls.AsNoTracking().ToListAsync();
            return response;
        }

        public async Task<List<BtSaleDtl>> GetByIdDtl(string code)
        {
            var response = await db.BtSaleDtls.AsNoTracking().Where(x=>x.SaleCode == code).ToListAsync();
            return response;
        }

        public async Task<string> CreateDtl(BtSaleDtl saledtl)
        {
            try
            {
                await db.BtSaleDtls.AddAsync(saledtl);
                await db.SaveChangesAsync();

                bool createAudit = await auditLog.CreateAuditLog("BT_SALE_DTL", "SaleCode", "admin", "admin");
                if (createAudit) return "Created Successfully";
                else return "Error in Audit";
            }
            catch
            {
                return "Error in Create";
            }
        }
    }
}
