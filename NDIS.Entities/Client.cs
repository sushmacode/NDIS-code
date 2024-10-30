namespace NDIS.Entities
{
    public class Client
    {
        public int ClientId { get; set; }

        public int? CompanyId { get; set; }

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string? DateOfBirth { get; set; }

        public int? GenderId { get; set; }
        public int IsActive { get; set; }
        public int StateId { get; set; }

        public string? MobileNumber { get; set; }

        public string? EmailId { get; set; }

        public string? ClientAddress { get; set; }

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
