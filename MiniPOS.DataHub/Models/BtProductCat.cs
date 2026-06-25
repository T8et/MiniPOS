using System;
using System.Collections.Generic;

namespace MiniPOS.DataHub.Models;

public partial class BtProductCat
{
    public int CatProductId { get; set; }

    public string CatProductCode { get; set; } = null!;

    public string? CatProductDesc { get; set; }
}
