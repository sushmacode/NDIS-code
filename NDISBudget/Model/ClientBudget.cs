using System;
using System.Collections.Generic;

namespace NDISBudget.Model;

public partial class ClientBudget
{
    public int ClientBudgetId { get; set; }

    public string? CompanyId { get; set; }

    public int? ClientId { get; set; }

    public decimal? ProposedBudget { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? ProviderId { get; set; }

    public string? Info { get; set; }

    public DateTime? CreatedDate { get; set; }
}
