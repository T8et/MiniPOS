using System;
using System.Collections.Generic;

namespace MiniPOS.DataHub.Models;

public partial class BtProduct
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = null!;

    public string? ProductDesc { get; set; }

    public decimal? ProductPrice { get; set; }

    public string? CatProductCode { get; set; }
}
