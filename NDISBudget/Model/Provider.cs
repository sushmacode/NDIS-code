using System;
using System.Collections.Generic;

namespace NDISBudget.Model;

public partial class Provider
{
    public int ProviderId { get; set; }

    public string? ProviderName { get; set; }

    public decimal? FundingAmount { get; set; }
}
