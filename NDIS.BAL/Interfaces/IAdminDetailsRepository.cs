using NDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.BAL.Interfaces
{
    public interface IAdminDetailsRepository : IGenericRepository<AdminDetails>
    {
        Task<AdminDetails> CheckAdminLogin(AdminDetails entity);
    }
}
