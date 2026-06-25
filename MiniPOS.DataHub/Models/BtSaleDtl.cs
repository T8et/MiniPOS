using System;
using System.Collections.Generic;

namespace MiniPOS.DataHub.Models;

public partial class BtSaleDtl
{
    public int SaleDetailId { get; set; }

    public string SaleCode { get; set; } = null!;

    public string? ProductCode { get; set; }

    public int? Qnty { get; set; }

    public decimal? UnitPrice { get; set; }
}
