using NDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.BAL.Interfaces
{
    public interface IProviderRepository : IGenericRepository<Provider>
    {
        Task<List<GetAdminProviderListResponse>> GetAdminProviderList(GetAdminProviderListParams p);

        Task<AddProviderResponse> AddProviderAsync(Provider c);
    }
}
