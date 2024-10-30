using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.Entities
{
    public class CompanyEntity
    {
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "Please enter CompanyName")]
        public string? CompanyName { get; set; }
        [Required(ErrorMessage = "Please enter CompanyType")]
        public int? CompanyTypeId { get; set; }
        public int? IsActive { get; set; }

        public string? EmailId { get; set; }

        public string? ContactNumber { get; set; }

        public string? Address { get; set; }

        [Required(ErrorMessage = "Please enter UserName")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        public string? Password { get; set; }

        public string? CreatedDate { get; set; }
    }
}
