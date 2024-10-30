using NDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.BAL.Interfaces
{
    public interface ICompanyRepository : IGenericRepository<CompanyEntity>
    {
        Task<List<GetAdminCompanyListResponse>> GetAdminCompanyList(GetAdminCompanyListParams p);
        Task<IEnumerable<CompanyEntity>> GetddlCompany();
        Task<IEnumerable<CompanyTypes>> GetddlCompanyTypes();
        Task<AddCompanyResponse> AddCompanyAsync(CompanyEntity c);

        Task<CompanyEntity> CheckCompanyLogin(CompanyEntity entity);
    }
}
