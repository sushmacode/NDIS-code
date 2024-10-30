using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.Entities
{
    public class ClientSupportItem
    {
        public int ClientSupportItemId { get; set; }

        public int? CompanyId { get; set; }

        public int? ClientBudgetId { get; set; }
        public int? ProviderId { get; set; }

        public int? SupportItemId { get; set; }
        [Required(ErrorMessage ="Please enter Budget")]
        public decimal? ItemBudget { get; set; }
        [Required(ErrorMessage = "Please enter Price Per Hour")]
        public decimal? PricePerHour { get; set; }
        public string? SupportInfo { get; set; }

        public string? SpecialConditions { get; set; }
        [Required(ErrorMessage = "Please enter StartDate")]
        public string? StartDate { get; set; }

        [Required(ErrorMessage = "Please enter EndDate")]
        public string? EndDate { get; set; }
        [Required(ErrorMessage = "Please select WeekDay")]
        public string? WeekDayIds { get; set; }
        [Required(ErrorMessage = "Please select Frequency")]
        public int? FrequencyId { get; set; }

        public decimal? DayHoursCount { get; set; }

        public DateTime? CreatedDate { get; set; }
        [Required(ErrorMessage = "Please enter ShiftStartTime")]
        public string? ShiftStartTime { get; set; }
        [Required(ErrorMessage = "Please enter ShiftEndTime")]
        public string? ShiftEndTime { get; set; }
        public int? IsActive { get; set; }
    }
}
