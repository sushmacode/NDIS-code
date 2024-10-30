using System;
using System.Collections.Generic;

namespace NDISBudget.Model;

public partial class ClientSupportItem
{
    public int ClientSupportItemId { get; set; }

    public int? CompanyId { get; set; }

    public int? ClientBudgetId { get; set; }

    public int? SupportItemId { get; set; }

    public decimal? ItemBudget { get; set; }

    public string? SupportInfo { get; set; }

    public string? SpecialConditions { get; set; }

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public int? WeekDayId { get; set; }

    public int? FrequencyId { get; set; }

    public int? DayHoursCount { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ShiftStartTime { get; set; }

    public string? ShiftEndTime { get; set; }
}
