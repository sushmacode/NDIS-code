using System;
using System.Collections.Generic;

namespace NDISBudget.Model;

public partial class Company
{
    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public int? CompanyTypeId { get; set; }

    public string? EmailId { get; set; }

    public string? ContactNumber { get; set; }

    public string? Address { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? CreatedDate { get; set; }
}
