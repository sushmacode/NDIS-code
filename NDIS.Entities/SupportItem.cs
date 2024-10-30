using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.Entities
{
    public class SupportItem
    {
        public int SupportItemId { get; set; }
        public int SupportCategoryId { get; set; }
        public int DayMandatoryId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? SupportItemName { get; set; }
        public string CustomName { get; set; }
        public string? SupportItemNumber { get; set; }

        public decimal? ItemPrice { get; set; }

        public string? Usage { get; set; }

        public string? Info { get; set; }
        public List<SupportCategory> SupportCategories { get; set; }
    }
}
