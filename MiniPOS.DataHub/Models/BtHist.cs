using System;
using System.Collections.Generic;

namespace MiniPOS.DataHub.Models;

public partial class BtHist
{
    public int AuditId { get; set; }

    public string AuditCode { get; set; } = null!;

    public string? EntityName { get; set; }

    public string? FieldName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }
}
