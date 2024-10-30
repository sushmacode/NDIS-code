using System;
using System.Collections.Generic;

namespace NDISBudget.Model;

public partial class SupportItem
{
    public int SupportItemId { get; set; }

    public string? SupportItemName { get; set; }

    public decimal? ItemPrice { get; set; }

    public string? Usage { get; set; }

    public string? Info { get; set; }
}
