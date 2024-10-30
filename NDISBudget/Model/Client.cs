using System;
using System.Collections.Generic;

namespace NDISBudget.Model;

public partial class Client
{
    public int ClientId { get; set; }

    public int? CompanyId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? DateOfBirth { get; set; }

    public int? GenderId { get; set; }

    public string? MobileNumber { get; set; }

    public string? EmailId { get; set; }

    public string? ClientAddress { get; set; }

    public DateTime? CreatedDate { get; set; }
}
