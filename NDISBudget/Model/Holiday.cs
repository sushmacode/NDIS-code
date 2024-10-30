using System;
using System.Collections.Generic;

namespace NDISBudget.Model;

public partial class Holiday
{
    public int HolidayId { get; set; }

    public DateOnly? HolidayDate { get; set; }

    public DateOnly? Year { get; set; }
}
