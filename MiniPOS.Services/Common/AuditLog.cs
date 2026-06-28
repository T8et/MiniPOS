using Microsoft.EntityFrameworkCore;
using MiniPOS.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPOS.Services.Common
{
    public class AuditLog
    {
        AppDBContext db;
        public AuditLog()
        {
            db = new AppDBContext();
        }

        public async Task<bool> CreateAuditLog(string entityname, string fieldname, string cruser, string mduser)
        {
            string auditcode = await AuditCodeGeneration();

            BtHist history = new BtHist();
            history.AuditCode = auditcode;
            history.EntityName = entityname;
            history.FieldName =  fieldname;
            history.CreatedBy = cruser;
            history.CreatedOn = DateTime.Now;
            history.ModifiedBy = mduser;
            history.ModifiedOn = DateTime.Now;

            try
            {
                db.BtHists.Add(history);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> AuditCodeGeneration()
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
