using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.Entities
{
    public class Provider
    {
        public int ProviderId { get; set; }
        [Required(ErrorMessage = "Please enter ProviderName")]
        public string? ProviderName { get; set; }

        public int? IsActive { get; set; }

        public string? EmailId { get; set; }
        [Required(ErrorMessage = "Please enter ContactNumber")]
        public string? ContactNumber { get; set; }

        public string? Address { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
