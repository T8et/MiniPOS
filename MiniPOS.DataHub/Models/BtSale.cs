using System;
using System.Collections.Generic;

namespace MiniPOS.DataHub.Models;

public partial class BtSale
{
    public int SaleId { get; set; }

    public string SaleCode { get; set; } = null!;

    public DateTime? SaleDate { get; set; }

    public decimal? SaleTotalAmt { get; set; }
}
