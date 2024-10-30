using NDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.BAL.Interfaces
{


    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<List<GetAdminClientsListResponse>> GetAdminClientsList(GetAdminClientsListParams p);
        Task<List<Client>> GetCompanyClientsAsync(int? CompanyId);
        Task<List<Client>> GetCompanyClientsdllAsync(int? CompanyId);

        Task<List<Provider>> GetddlProvider();
        Task<List<SupportItem>> GetddlNASupportItemBySCId(int id);
        Task<List<AusState>> GetddlStates();
    }

}
