using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.Entities
{
    public class AdminDetails
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Pwd { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
