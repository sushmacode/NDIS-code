using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.Entities
{
    public class ClientBudget
    {
        public int ClientBudgetId { get; set; }

        public string? CompanyId { get; set; }
        public string? NDISRefno { get; set; }

        public int? ClientId { get; set; }
        public int? StateId { get; set; }

        public decimal? ProposedBudget { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? ProviderId { get; set; }
        public int? IsActive { get; set; }

        public string? Info { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string? SupportCoordinator { get; set; }
        public string? SCCompany { get; set; }
        public string? SCPhone { get; set; }
        public string? SCEmail { get; set; }
        public string? PlanManager { get; set; }
        public string? PMPhone { get; set; }
        public string? PMEmail { get; set; }
    }
}
